using UnityEngine;
using Abilities;
using Map;

namespace Core.Input
{
    public class ClickAbilityTarget : IClickAction
    {
        public ClickAbilityTarget() { }
        IAbilityContainer _abilityContainer { get; init; }
        CustomEvent _targetSelectedEvent { get; init; }

        public ClickAbilityTarget(IAbilityContainer abilityContainer)
        {
            _abilityContainer = abilityContainer;
        }

        public ClickAbilityTarget(IAbilityContainer abilityContainer,
            CustomEvent targetSelectedEvent)
        {
            _abilityContainer = abilityContainer;
            _targetSelectedEvent = targetSelectedEvent;
        }

        public bool CanBeUsedOnTile(ITileClickData tile)
        {
            return _abilityContainer.TileHasValidTarget(tile);
        }

        public void ProcessClick(ITileClickData tile)
        {
            _abilityContainer.UseAbility(tile);
            _targetSelectedEvent?.Invoke();
        }
    }

}