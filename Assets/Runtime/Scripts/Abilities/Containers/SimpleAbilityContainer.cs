using Map;
using UnityEngine;

namespace Abilities
{
    public class SimpleAbilityContainer : AbilityContainer
    {
        public override bool canBeUsed => true;
        protected override IAbility ability => _ability;
        IAbility _ability { get; init; }

        public SimpleAbilityContainer(IAbility ability)
        {
            _ability = ability;
        }

        public override void UpdateAbilityCounter(IAbilityCounterHandler handler)
        {
            handler.HideAbilityCounter();
        }

        public override void UseAbility(IAbilityTarget target)
        {
            _ability.Use(target);
        }

        public override bool TileHasValidTarget(ITileClickData tile)
        {
            return _ability.TileHasValidTarget(tile);
        }
    }

}