using Map;

namespace Abilities
{
    public class AoeAbility : AbstractAbility
    {
        protected override IIconData template => _template;
        public override bool fitForMainSlot => true;

        AoeAbilityTemplate _template;

        [InjectField] AoeAbilityController _controller;

        public AoeAbility(AoeAbilityTemplate template)
        {
            _template = template;
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }

        public override bool TileHasValidTarget(IAbilityUser user, ITileClickData tile)
        {
            throw new System.NotImplementedException();
        }

        public override void Use(IAbilityUser user, IAbilityTarget target)
        {
            throw new System.NotImplementedException();
        }

        public override IAbilityTarget SelectTarget(ITileClickData tile)
        {
            throw new System.NotImplementedException();
        }
    }
}