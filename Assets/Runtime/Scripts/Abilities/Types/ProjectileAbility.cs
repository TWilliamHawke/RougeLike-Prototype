using System.Linq;
using Effects;
using Entities;
using Map;
using UnityEngine;

namespace Abilities
{
    public class ProjectileAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        ProjectileAbilityTemplate _template;
        IAbilityTarget _target;
        PositionController _userPosition;

        [InjectField] ProjectileController _controller;
        public Vector3 userPosition => _userPosition.position;
        public ProjectileTemplate projectileTemplate => _template.projectile;
        public ProjectileAbilityTemplate abilityTemplate => _template;

        public ProjectileAbility(ProjectileAbilityTemplate template)
        {
            _template = template;
        }

        public void ApplyEffect(IAbilityTarget target)
        {
            var effectsStorage = target.GetEntityComponent<EffectsStorage>();
            var effects = _template.GetEffects();
            foreach (var effect in effects)
            {
                effect.ApplyEffect(effectsStorage, _template);
            }
        }

        public void PlayImpactSound()
        {
            var soundController = _target.GetEntityComponent<AudioEffectsController>();
            soundController.PlaySound(_template.projectile.impactSound);
        }

        public override void Use(IAbilityUser user, IAbilityTarget target)
        {
            _target = target;
            _userPosition = user.GetEntityComponent<PositionController>();
            _controller.UseAbility(target, this);
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            return _template.GetDescription(abilityModifiers);
        }

        public override bool TileHasValidTarget(IAbilityUser user, ITileClickData tile)
        {
            //TODO add visibility check
            return tile.entitiesOnTile.Any(entity => entity is IAbilityTarget);
        }

        public override IAbilityTarget SelectTarget(ITileClickData tile)
        {
            var target = tile.entitiesOnTile.First(entity => entity is IAbilityTarget);
            return target as IAbilityTarget;
        }
    }
}