using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    [CreateAssetMenu(fileName = "QuickBarDataStorage", menuName = "Abilities/QuickBarDataStorage", order = 1)]
    public class QuickBarDataStorage : ScriptableObject
    {
        const int MAX_QUICK_ABILITIES = 10;

        public event UnityAction OnQuickBarChange;

        public IAbilityContainer mainAbility => _mainAbility;

        IAbilityContainer _mainAbility;

        IAbilityContainer[] _quickAbilities = new IAbilityContainer[MAX_QUICK_ABILITIES];
        List<IObserver<IAbilityContainer>> _observers = new();

        private void OnEnable()
        {
            _mainAbility = null;
            for (int i = 0; i < _quickAbilities.Length; i++)
            {
                _quickAbilities[i] = null;
            }
        }

        public void AddSlotObserver(IObserver<IAbilityContainer> observer)
        {
            _observers.Add(observer);

            for (int i = 0; i < _quickAbilities.Length; i++)
            {
                if (_quickAbilities[i] == null) continue;
                observer.AddToObserve(_quickAbilities[i]);
            }

            if (_mainAbility == null) return;
            observer.AddToObserve(_mainAbility);
        }

        public void RemoveSlotObserver(QuickBarSpellObserver quickBarSpellObserver)
        {
            _observers.Remove(quickBarSpellObserver);
        }

        public void SetQuickAbility(int index, IAbilityContainer ability)
        {
            if (!IndexIsCorrect(index))return;

            TryRemoveQuickAbility(index);
            _observers.ForEach(observer => observer.AddToObserve(ability));
            _quickAbilities[index] = ability;
            OnQuickBarChange?.Invoke();
        }

        public void SetMainAbility(IAbilityContainer ability)
        {
            TryRemoveMainAbility();
            _observers.ForEach(observer => observer.AddToObserve(ability));
            _mainAbility = ability;
            OnQuickBarChange?.Invoke();
        }

        public bool TryGetQuickAbility(int index, out IAbilityContainer ability)
        {
            ability = default;
            if (IndexIsCorrect(index) && _quickAbilities[index] != null)
            {
                ability = _quickAbilities[index];
                return true;
            }
            return false;
        }

        public void RemoveAbility(IAbilityContainer ability)
        {
            bool isSuccess = false;

            for (int i = 0; i < _quickAbilities.Length; i++)
            {
                if (_quickAbilities[i] != ability) continue;
                isSuccess = TryRemoveQuickAbility(i) || isSuccess;
            }

            if (_mainAbility != ability) return;
            isSuccess = TryRemoveMainAbility() || isSuccess;

            if (!isSuccess) return;
            OnQuickBarChange?.Invoke();
        }

        private bool TryRemoveQuickAbility(int index)
        {
            if (!IndexIsCorrect(index) || _quickAbilities[index] == null) return false;
            _observers.ForEach(observer => observer.RemoveFromObserve(_quickAbilities[index]));
            _quickAbilities[index] = null;
            return true;
        }

        private bool TryRemoveMainAbility()
        {
            if (_mainAbility == null) return false;
            _observers.ForEach(observer => observer.RemoveFromObserve(_mainAbility));
            _mainAbility = null;
            return true;
        }

        private bool IndexIsCorrect(int index)
        {
            return index >= 0 && index < MAX_QUICK_ABILITIES;
        }

    }
}