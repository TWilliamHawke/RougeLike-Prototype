using Effects;
using Entities;
using Map;
using UnityEngine;

namespace Abilities
{
    public class MeleeAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        MeleeAbilityTemplate _template;
        IEffectSource _effectSource;

        PositionController _userPosition;
        IAbilityUser _abilityUser;

        [InjectField] MeleeAttackController _controller;
        [InjectField] AbilityEfffectsHandler _effectsHandler;

        public Vector3 userPosition => _userPosition.position;
        public override bool fitForMainSlot => true;

        public MeleeAbility(MeleeAbilityTemplate template, IEffectSource effectSource)
        {
            _effectSource = effectSource;
            _template = template;
        }

        public MeleeAbility(MeleeAbilityTemplate template) : this(template, template)
        {
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }

        public override bool TileHasValidTarget(IAbilityUser user, ITileClickData tile)
        {
            var positionData = user.GetEntityComponent<PositionController>();
            if (positionData == null) return false;
            float deltaX = positionData.position.x - tile.intPosition.x;
            float deltaY = positionData.position.y - tile.intPosition.y;
            return Mathf.Abs(deltaX) <= 1 && Mathf.Abs(deltaY) <= 1;
        }

        public override void Use(IAbilityUser user, IAbilityTarget target)
        {
            _abilityUser = user;
            _userPosition = user.GetEntityComponent<PositionController>();
            _controller.UseAbility(target, this);
        }

        public override IAbilityTarget SelectTarget(ITileClickData tile)
        {
            throw new System.NotImplementedException();
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