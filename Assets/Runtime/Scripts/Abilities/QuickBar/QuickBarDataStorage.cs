using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    public class QuickBarDataStorage : ScriptableObject
    {
        const int MAX_QUICK_ABILITIES = 10;

        public event UnityAction OnQuickBarChange;
        public event UnityAction<IAbilityContainer> OnAbilityAdded;
        public event UnityAction<IAbilityContainer> OnAbilityRemoved;
        public event UnityAction OnMainAbilityChanged;
        public event UnityAction OnInit;

        public IAbilityContainer mainAbility => _mainAbility;
        public IAbilityContainer movementAbility => _movementAbility;

        IAbilityContainer _mainAbility;
        IAbilityContainer _movementAbility;

        IAbilityContainer[] _quickAbilities = new IAbilityContainer[MAX_QUICK_ABILITIES];

        public void Init()
        {
            OnInit?.Invoke();
        }

        public void Reset()
        {
            _mainAbility = null;
            _movementAbility = null;
            OnInit = null;
            OnAbilityAdded = null;
            OnAbilityRemoved = null;
            OnQuickBarChange = null;
            for (int i = 0; i < _quickAbilities.Length; i++)
            {
                _quickAbilities[i] = null;
            }
        }

        public void SetQuickAbility(int index, IAbilityContainer ability)
        {
            if (!IndexIsCorrect(index)) return;

            TryRemoveQuickAbility(index);
            _quickAbilities[index] = ability;
            OnAbilityAdded?.Invoke(ability);
            OnQuickBarChange?.Invoke();
        }

        public void SetMovementAbility(IAbilityContainer ability, IAbilityUser user)
        {
            _movementAbility = ability;
            _movementAbility.SelectBy(user);
            OnQuickBarChange?.Invoke();
        }

        public void SetMainAbility(IAbilityContainer ability, IAbilityUser user)
        {
            TryRemoveMainAbility();
            _mainAbility = ability;
            _mainAbility.SelectBy(user);
            OnAbilityAdded?.Invoke(ability);
            OnQuickBarChange?.Invoke();
            OnMainAbilityChanged?.Invoke();
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
            OnAbilityRemoved?.Invoke(_quickAbilities[index]);
            _quickAbilities[index] = null;
            return true;
        }

        private bool TryRemoveMainAbility()
        {
            if (_mainAbility == null) return false;
            OnAbilityRemoved?.Invoke(_mainAbility);
            _mainAbility = null;
            return true;
        }

        private bool IndexIsCorrect(int index)
        {
            return index >= 0 && index < MAX_QUICK_ABILITIES;
        }

        public IEnumerable<IAbilityContainer> GetAbilities()
        {
            if (_mainAbility != null) yield return _mainAbility;
            for (int i = 0; i < _quickAbilities.Length; i++)
            {
                if (_quickAbilities[i] == null) continue;
                yield return _quickAbilities[i];
            }
        }
    }
}