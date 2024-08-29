using Core.Input;
using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Abilities
{
    [RequireComponent(typeof(ComponentInjector))]
    public class TargetSelectionHandler : MonoBehaviour
    {
        [InjectField] InputController _inputController;
        [InjectField] Player _player;

        IAbilityWithTarget _selectedAbility;
        AbilityController _playerController;

        void OnDestroy()
        {
            _inputController.targetSelection.Select.started -= SelectTarget;
            _inputController.targetSelection.Return.started -= CancelSelection;
        }

        //Used in Unity Editor
        public void FindPlayerComponents()
        {
            _playerController = _player.GetComponent<AbilityController>();
            _inputController.targetSelection.Select.started += SelectTarget;
            _inputController.targetSelection.Return.started += CancelSelection;
            _playerController.OnTargetSelectionStart += SelectAbility;
        }

        private void SelectAbility(IAbilityWithTarget ability)
        {
            _selectedAbility = ability;
            _inputController.SwitchToTargetSelection();
        }

        private void SelectTarget(InputAction.CallbackContext _)
        {
            foreach (var hit in _inputController.hoveredTileHits)
            {
                if (hit.collider.TryGetComponent<IAbilityTarget>(out var target))
                {
                    _selectedAbility.UseOnTarget(_playerController, target);
                    _inputController.SwitchToMainActionMap();
                    return;
                }
            }
        }

        private void CancelSelection(InputAction.CallbackContext _)
        {
            _inputController.SwitchToMainActionMap();
        }
    }
}