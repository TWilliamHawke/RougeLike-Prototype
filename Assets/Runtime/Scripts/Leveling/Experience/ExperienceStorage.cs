using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Leveling
{
	public class ExperienceStorage : ScriptableObject
	{
		[SerializeField] int _expPerLevelMult = 500;
		[SerializeField] int _levelCap = 99;

		public event UnityAction OnGettingExp;
		public event UnityAction OnLevelUp;

		long _playerExp;
		int _playerLevel = 1;

		public int playerLevel => _playerLevel;
		public long playerExp => _playerExp;

		private void OnEnable() {
			_playerExp = 0;
			_playerLevel = 1;
		}

		public void AddExp(int exp)
        {
			if(exp <= 0) return;
			if(_playerLevel >= _levelCap) return;

            _playerExp += exp;
            OnGettingExp?.Invoke();

            long expToNextLevelLeft = GetTotalExpToReachLevel(_playerLevel + 1) - _playerExp;

            if (expToNextLevelLeft > exp) return;
            _playerLevel++;
            OnLevelUp?.Invoke();
        }

        public long GetTotalExpToReachLevel(int level)
		{
			if(level == 1) return 0L;
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
	}
}

