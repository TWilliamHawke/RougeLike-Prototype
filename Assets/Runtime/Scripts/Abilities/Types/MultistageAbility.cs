using System.Collections.Generic;
using System.Linq;
using Map;

namespace Abilities
{
    public class MultistageAbility : AbstractAbility
    {
        protected override IIconData template => _template;
        public override bool fitForMainSlot => false;

        MultistageAbilityTemplate _template;

        List<IAbility> _abilities;

        [InjectField] MultistageAbilityController _controller;

        public MultistageAbility(MultistageAbilityTemplate template)
        {
            _template = template;
        }

        public override void Use(IAbilityUser user, IAbilityTarget tile)
        {
            throw new System.NotImplementedException();
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }

        public override bool TileHasValidTarget(IAbilityUser user, ITileClickData tile)
        {
            return _abilities.All(ability => ability.TileHasValidTarget(user, tile));
        }

        public override IAbilityTarget SelectTarget(ITileClickData tile)
        {
            throw new System.NotImplementedException();
        }
    }
}