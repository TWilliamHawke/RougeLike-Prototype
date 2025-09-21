using System.Linq;
using System.Text;
using Effects;
using Map;

namespace Abilities
{
    public class SelfAbility : AbstractAbility
    {
        protected override IIconData template => _template;
        public override bool fitForMainSlot => false;

        IEffectSource _template;
        [InjectField] SelfAbilityController _controller;

        public SelfAbility(IEffectSource template)
        {
            _template = template;
        }

        public override void Select(IAbilityUser user, IAbilityContainer container)
        {
            IAbilityTarget target = user.GetEntityComponent<IAbilityTarget>();
            if (target is null) return;
            container.UseAbility(target);
        }

        public override void Use(IAbilityUser _, IAbilityTarget target)
        {
            _controller.ApplyEffects(_template.GetEffects(), target, _template);
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            var sb = new StringBuilder();

            foreach (var effectData in _template.GetEffects())
            {
                sb.AppendLine(effectData.GetDescription(abilityModifiers));
            }

            return sb.ToString();
        }

        public override bool TileHasValidTarget(IAbilityUser user, ITileClickData _)
        {
            return user is IAbilityTarget;
        }

        public override IAbilityTarget SelectTarget(ITileClickData tile)
        {
            var target = tile.entitiesOnTile.First(entity => entity is IAbilityTarget);
            return target as IAbilityTarget;
        }
    }
}