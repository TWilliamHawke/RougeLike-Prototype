using Abilities;

namespace Core.Input
{
    public class ClickAbilityTarget : IClickAction
    {
        public ClickAbilityTarget() { }
        IAbilityContainer _abilityContainer;
        CustomEvent _targetSelectedEvent;

        public ClickAbilityTarget(IAbilityContainer abilityContainer,
            CustomEvent targetSelectedEvent)
        {
            _abilityContainer = abilityContainer;
            _targetSelectedEvent = targetSelectedEvent;
        }

        bool IClickAction.Condition()
        {
            return true;
        }

        void IClickAction.ProcessClick()
        {
            _targetSelectedEvent?.Invoke();
        }
    }

}