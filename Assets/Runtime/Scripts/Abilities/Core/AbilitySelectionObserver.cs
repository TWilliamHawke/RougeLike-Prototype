using Core.Input;
using Entities.PlayerScripts;
using Map;
using UnityEngine;

namespace Abilities
{
    public class AbilitySelectionObserver : MonoBehaviour
    {
        [InjectField] ITaskPanelController _taskPanelController;
        [InjectField] ClickStateMachine _clickStateMachine;
        [InjectField] Player _player;

        [SerializeField] LocalString _abilitySelectedText;
        [SerializeField] QuickBarDataStorage _quickBarDataStorage;
        [SerializeField] CustomEvent _targetSelectedEvent;
        [SerializeField] Injector _screenPositionReader;
        
        public void Subscribe()
        {
            var abilityController = _player.GetComponent<AbilityController>();
            abilityController.OnAbilitySelected += StartTargetSelection;
        }

        private void StartTargetSelection(IAbilityContainer ability)
        {
            if (!ability.canBeUsed) return;
            if (_quickBarDataStorage.mainAbility == ability) return;
            if (_quickBarDataStorage.movementAbility == ability) return;
            CreateAbilityTask(ability);
        }

        private void CreateAbilityTask(IAbilityContainer ability)
        {
            TaskData task = new()
            {
                displayName = ability.displayName,
                taskText = _abilitySelectedText,
                icon = ability.icon,
                isDone = true,
            };

            _taskPanelController.ChangeTask(task);
            AbilityClickActions actions = new(ability, _targetSelectedEvent);
            _screenPositionReader.AddInjectionTarget(actions);
            _clickStateMachine.ReplaceAcionList(actions);
        }

        //used in editor
        public void FinalizeSelection()
        {
            _taskPanelController.ResetTask();
            _clickStateMachine.ResetActionList();
        }
    }
}
