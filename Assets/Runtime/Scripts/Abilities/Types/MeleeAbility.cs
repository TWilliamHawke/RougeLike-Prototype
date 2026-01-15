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
        [InjectField] AbilityEfffectsController _effectsController;

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
            bool hasAnyTarget = tile.entitiesOnTile
                .Any(entity => entity is IAbilityTarget);
            if (!hasAnyTarget) return false;
            if (_userPosition == null) return false;
            return _template.HitTargetIsValid(userPosition, tile.intPosition);
        }

        public override void Use(IAbilityTarget target)
        {
            _controller.UseAbility(target, this);
        }

        //TODO add faction check and corpse check
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
            _effectsController.ApplyEffects(_abilityUser, target, _effectSource);
        }

        public void ApplyEffect(Vector3 hitPosition, IMapNodeStorage mapNodeStorage)
        {
            Vector3 relativePosition = hitPosition - userPosition;
            foreach(Vector3Int targetPosition in _template.GetTargetPositions(relativePosition))
            {
                Vector3Int nodePosition = _userPosition.intPosition + targetPosition;
                if (mapNodeStorage.TryGetNode(nodePosition, out var node))
                {
                    var target = SelectTarget(node);
                    if (target is null) continue;
                    ApplyEffect(target);
                }
            }
        }
    }
}