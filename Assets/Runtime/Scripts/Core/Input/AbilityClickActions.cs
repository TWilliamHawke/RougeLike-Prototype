using System.Collections;
using System.Collections.Generic;
using Abilities;

namespace Core.Input
{
    public class AbilityClickActions : IClickActionList, IInjectionTarget
    {
        [InjectField] IScreenPositionReader _screenPositionReader;

        List<IClickAction> _clickActions = new();
        IAbilityContainer _abilityContainer;
        CustomEvent _targetSelectedEvent;

        public bool waitForAllDependencies => false;

        public AbilityClickActions(IAbilityContainer abilityContainer, CustomEvent targetSelectedEvent)
        {
            _abilityContainer = abilityContainer;
            _targetSelectedEvent = targetSelectedEvent;
        }

        void IInjectionTarget.FinalizeInjection()
        {
            _clickActions.Add(new ClickUI(_screenPositionReader));
            _clickActions.Add(new ClickAbilityTarget(
                _abilityContainer, _targetSelectedEvent));
        }

        public void CleanUp()
        {
        }

        public IEnumerable<IClickAction> GetActions()
        {
            return _clickActions;
        }
    }
}