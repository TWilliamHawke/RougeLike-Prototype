using Effects;
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

        public abstract bool fitForMainSlot { get; }
        public abstract IAbilityTarget SelectTarget(ITileClickData tile);
        public abstract void Use(IAbilityUser user, IAbilityTarget target);
        public abstract string GetDescription(AbilityModifiers abilityModifiers);
        public abstract bool TileHasValidTarget(IAbilityUser user, ITileClickData tile);

        public virtual void Select(IAbilityUser user, IAbilityContainer container)
        {
            user.SelectAbility(container);
        }

        public void BindEffectSource(IEffectSource effectSource)
        {
            _effectSource = effectSource;
        }
    }
}