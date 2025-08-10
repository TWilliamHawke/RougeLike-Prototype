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

        [SerializeField] CustomEvent _targetSelectedEvent;

        public void Subscribe()
        {
            var abilityController = _player.GetComponent<AbilityController>();
            abilityController.OnAbilitySelected += StartTargetSelection;
        }

        private void StartTargetSelection(IAbilityContainer ability)
        {
            if (!ability.canBeUsed) return;
            CreateAbilityTask(ability);
        }

        private void CreateAbilityTask(IAbilityContainer ability)
        {
            TaskData task = new()
            {
                displayName = ability.displayName,
                taskText = "Select ability Target",
                icon = ability.icon,
                isDone = true,
            };

            _taskPanelController.ChangeTask(task);
            AbilityClickActions actions = new(ability, _targetSelectedEvent);
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
