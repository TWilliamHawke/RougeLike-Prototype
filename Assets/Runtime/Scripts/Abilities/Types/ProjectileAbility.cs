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
        IAbilityUser _abilityUser;
        IEffectSource _effectSource;

        [InjectField] ProjectileController _controller;
        [InjectField] AbilityEfffectsHandler _effectsHandler;

        public Vector3 userPosition => _userPosition.position;
        public ProjectileTemplate projectileTemplate => _template.projectile;
        public ProjectileAbilityTemplate abilityTemplate => _template;

        public ProjectileAbility(ProjectileAbilityTemplate template) : this(template, template)
        {
        }

        public ProjectileAbility(ProjectileAbilityTemplate template, IEffectSource effectSource)
        {
            _effectSource = effectSource;
            _template = template;
        }

        public void ApplyEffect(IAbilityTarget target)
        {
            _effectsHandler.ApplyEffects(_abilityUser, target, _effectSource);
        }

        public void PlayImpactSound()
        {
            var soundController = _target.GetEntityComponent<AudioEffectsController>();
            soundController.PlaySound(_template.projectile.impactSound);
        }

        public override void Use(IAbilityUser user, IAbilityTarget target)
        {
            _abilityUser = user;
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