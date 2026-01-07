using System.Collections;
using System.Collections.Generic;
using Abilities;

namespace Core.Input
{
    public class AbilityClickActions : IClickActionList
    {
        List<IClickAction> _clickActions = new();
        IAbilityContainer _abilityContainer;

        public AbilityClickActions(IAbilityContainer abilityContainer, CustomEvent targetSelectedEvent)
        {
            _abilityContainer = abilityContainer;
            _clickActions.Add(new ClickUI());
            _clickActions.Add(new ClickAbilityTarget(
                abilityContainer, targetSelectedEvent));
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