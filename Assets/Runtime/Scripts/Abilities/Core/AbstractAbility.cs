using Effects;
using Entities;
using Map;
using UnityEngine;

namespace Abilities
{
    public abstract class AbstractAbility : IAbility, IInjectionTarget
    {
        public Sprite icon => template.icon;
        public string displayName => template.displayName;

        public bool waitForAllDependencies => false;

        protected abstract IIconData template { get; }
        protected IEffectSource _effectSource;
        protected IAbilityUser _abilityUser;
        protected AudioEffectsController _audioEffectsController;

        public abstract bool fitForMainSlot { get; }
        public abstract IAbilityTarget SelectTarget(ITileClickData tile);
        public abstract string GetDescription(AbilityModifiers abilityModifiers);
        public abstract bool TileHasValidTarget(ITileClickData tile);
        public abstract void Use(IAbilityTarget target);

        public virtual void Select(IAbilityContainer container)
        {
            _abilityUser.SelectAbility(container);
        }

        public void BindEffectSource(IEffectSource effectSource)
        {
            _effectSource = effectSource;
        }

        public virtual void BindAbilityUser(IAbilityUser user)
        {
            _abilityUser = user;
            _audioEffectsController = user.GetEntityComponent<AudioEffectsController>();
        }
    }
}