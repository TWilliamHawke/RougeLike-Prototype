using Map;
using UnityEngine;

namespace Abilities
{
    public abstract class AbilityContainer : IAbilityContainer
    {
        public abstract bool canBeUsed { get; }
        public virtual string displayName => ability.displayName;
        public virtual Sprite icon => ability.icon;
        public bool fitForMainSlot => ability.fitForMainSlot;

        protected abstract IAbility ability { get; }

        public abstract void UpdateAbilityCounter(IAbilityCounterHandler handler);
        public abstract void UseAbility(IAbilityTarget target);
        public abstract bool TileHasValidTarget(ITileClickData tile);

        public void SelectBy(IAbilityUser user)
        {
            ability.BindAbilityUser(user);
            ability.Select(this);
        }

        public void UseAbility(ITileClickData tile)
        {
            var target = ability.SelectTarget(tile);
            UseAbility(target);
        }


    }
}