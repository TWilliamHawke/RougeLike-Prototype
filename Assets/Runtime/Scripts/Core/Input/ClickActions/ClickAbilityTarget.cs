using Abilities;

namespace Core.Input
{
    public class ClickAbilityTarget : IMouseClickAction
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

        bool IMouseClickAction.Condition()
        {
            return true;
        }

        void IMouseClickAction.ProcessClick()
        {
            _targetSelectedEvent?.Invoke();
        }
    }

}