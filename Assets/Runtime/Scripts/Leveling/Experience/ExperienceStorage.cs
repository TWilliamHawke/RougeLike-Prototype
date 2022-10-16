using System.Collections;
using System.Collections.Generic;
using Core.SaveSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Leveling
{
    public class ExperienceStorage : IInjectionTarget, IHaveSaveState, IDependency, IPermanentDependency
    {
        int _expPerLevelMult = 500;
        int _levelCap = 99;

        [InjectField] SavedDataStorage _savedDataStorage;

        public event UnityAction OnGettingExp;
        public event UnityAction OnLevelUp;
        public event UnityAction OnReadyForUse;

        long _playerExp;
        int _playerLevel = 1;
        const string EXP_SAVE_KEY = "player_exp";
        const string LEVEL_SAVE_KEY = "player_level";

        public int playerLevel => _playerLevel;
        public long playerExp => _playerExp;
        public bool waitForAllDependencies => false;

        public bool isReadyForUse => _savedDataStorage != null;

        private void OnEnable()
        {
            _playerExp = 0;
            _playerLevel = 1;
        }

        public void AddExp(int exp)
        {
            if (exp <= 0) return;
            if (_playerLevel >= _levelCap) return;

            _playerExp += exp;
            OnGettingExp?.Invoke();

            long expToNextLevelLeft = GetTotalExpToReachLevel(_playerLevel + 1) - _playerExp;

            if (expToNextLevelLeft > exp) return;
            _playerLevel++;
            OnLevelUp?.Invoke();
        }

        public long GetTotalExpToReachLevel(int level)
        {
            if (level == 1) return 0L;
            return (GetExpToNextLevel(1) + GetExpToNextLevel(level - 1)) * (level - 1) / 2;
        }

        public long GetExpToNextLevel(int currentLevel)
        {
            return currentLevel * _expPerLevelMult;
        }

        public long GetExpToNextLevel()
        {
            return GetExpToNextLevel(_playerLevel);
        }

        public void FinalizeInjection()
        {
            _savedDataStorage.Register(this);
            OnReadyForUse?.Invoke();
        }

        void IHaveSaveState.Save(ISaveManager saveManager)
        {
            saveManager.AddSaveState(LEVEL_SAVE_KEY, _playerLevel);
            saveManager.AddSaveState(EXP_SAVE_KEY, _playerExp);
        }

        void IHaveSaveState.Load(ILoadManager loadManager)
        {
            loadManager.GetSaveState(EXP_SAVE_KEY, ref _playerExp);
            loadManager.GetSaveState(LEVEL_SAVE_KEY, ref _playerLevel);
        }

        public void ClearState()
        {
            _savedDataStorage = null;
        }
    }
}

