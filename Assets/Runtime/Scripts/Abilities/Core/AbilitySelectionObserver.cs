using Core.Input;
using Map;
using UnityEngine;

namespace Abilities
{
    public class AbilitySelectionObserver : MonoBehaviour
    {
        [InjectField] ITaskPanelController _taskPanelController;
        [InjectField] ClickStateMachine _clickStateMachine;

        [SerializeField] CustomEvent _targetSelectedEvent;

        void Awake()
        {
            AbilityContainer.OnAbilitySelection += StartTargetSelection;
        }

        void OnDestroy()
        {
            AbilityContainer.OnAbilitySelection -= StartTargetSelection;
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
