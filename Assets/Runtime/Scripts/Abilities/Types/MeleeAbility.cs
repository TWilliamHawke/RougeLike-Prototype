using System.Linq;
using Entities;
using Map;
using UnityEngine;

namespace Abilities
{
    public class MeleeAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        MeleeAbilityTemplate _template;

        PositionController _userPosition;

        [InjectField] MeleeAttackController _controller;
        [InjectField] AbilityEfffectsHandler _effectsHandler;

        public Vector3 userPosition => _userPosition.position;
        public override bool fitForMainSlot => true;

        public MeleeAbility(MeleeAbilityTemplate template)
        {
            _template = template;
        }

        public void PlayAttackSound()
        {
            var soundController = _abilityUser.GetEntityComponent<AudioEffectsController>();
            soundController.PlaySound(_template.useSound);
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }

        public override void BindAbilityUser(IAbilityUser user)
        {
            base.BindAbilityUser(user);
            _userPosition = _abilityUser.GetEntityComponent<PositionController>();
        }

        public override bool TileHasValidTarget(ITileClickData tile)
        {
            bool hasAnyTarget = tile.entitiesOnTile.Any(entity => entity is IAbilityTarget);
            if (!hasAnyTarget) return false;
            if (_userPosition == null) return false;
            float deltaX = userPosition.x - tile.intPosition.x;
            float deltaY = userPosition.y - tile.intPosition.y;
            return Mathf.Abs(deltaX) <= 1 && Mathf.Abs(deltaY) <= 1;
        }

        public override void Use(IAbilityTarget target)
        {
            _controller.UseAbility(target, this);
        }

        public override IAbilityTarget SelectTarget(ITileClickData tile)
        {
            var target = tile.entitiesOnTile.FirstOrDefault(entity => entity is IAbilityTarget);
            return target as IAbilityTarget;
        }

        public void MoveUserBody(Vector3 position)
        {
            _userPosition.UpdateBodyPosition(position);
        }

        public void ApplyEffect(IAbilityTarget target)
        {
            _effectsHandler.ApplyEffects(_abilityUser, target, _effectSource);
        }
    }
}