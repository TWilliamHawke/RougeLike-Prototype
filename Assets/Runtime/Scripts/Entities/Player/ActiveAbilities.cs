using System;
using System.Collections;
using System.Collections.Generic;
using Core.Input;
using Effects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player
{
    [CreateAssetMenu(fileName = "ActiveAbilities", menuName = "Musc/ActiveAbilities")]
    public class ActiveAbilities : ScriptableObject
    {
        [SerializeField] InputController _inputController;

        IAbilityInstruction[] _activeAbilities = new IAbilityInstruction[10];

        AbilityController _playerController;

        IAbilityWithTarget _selectedAbility;

        public IAbilityInstruction this[int index]
        {
            get => index <= 10 ? _activeAbilities[index] : null;
            set => _activeAbilities[index] = value;
        }

        public void SetController(AbilityController abilityController)
        {
            if (abilityController is null) Debug.Log("Error");
            _playerController = abilityController;
            _playerController.OnTargetSelectionStart += SelectAbility;
            _inputController.targetSelection.Select.started += SelectTarget;
            _inputController.targetSelection.Return.started += CancelSelection;
        }

        public void UseAbility(int index)
        {
            if (index >= 10 || _activeAbilities[index] is null) return;

            _activeAbilities[index].UseAbility(_playerController);
        }

        public void UseAbility(Ability ability)
        {
            ability.SelectControllerUsage(_playerController);
        }

        public void RemoveAbilityAt(int index)
        {

        }

        void SelectAbility(IAbilityWithTarget ability)
        {
            _selectedAbility = ability;
            _inputController.SwitchToTargetSelection();
        }

        void SelectTarget(InputAction.CallbackContext _)
        {
            foreach (var hit in _inputController.hoveredTileHits)
            {
                if (hit.collider.TryGetComponent<IEffectTarget>(out var target))
                {
                    _selectedAbility.UseOnTarget(_playerController, target);
                    _inputController.SwitchToMainActionMap();
                    return;
                }
            }
        }

        void CancelSelection(InputAction.CallbackContext _)
        {
            _inputController.SwitchToMainActionMap();
        }



    }
}