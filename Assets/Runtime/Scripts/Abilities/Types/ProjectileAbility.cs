using System.Linq;
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
        [InjectField] AbilityEfffectsController _effectsController;

        public Vector3 userPosition => _userPosition.position;
        public ProjectileTemplate projectileTemplate => _template.projectile;
        public ProjectileAbilityTemplate abilityTemplate => _template;
        public override bool fitForMainSlot => true;

        public ProjectileAbility(ProjectileAbilityTemplate template)
        {
            _template = template;
        }

        //TODO fix infinity loop if IEffectSource.GetEffects is not implemented
        public void ApplyEffect(IAbilityTarget target)
        {
            _effectsController.ApplyEffects(_abilityUser, target, _effectSource);
        }

        public void PlayImpactSound()
        {
            var soundController = _target.GetEntityComponent<AudioEffectsController>();
            soundController.PlaySound(_template.projectile.impactSound);
        }

        public override void Use(IAbilityTarget target)
        {
            _target = target;
            _userPosition = _abilityUser.GetEntityComponent<PositionController>();
            _controller.UseAbility(target, this);
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            return _template.GetDescription(abilityModifiers);
        }

        public override bool TileHasValidTarget(ITileClickData tile)
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