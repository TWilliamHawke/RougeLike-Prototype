using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Core.Input;
using Map;

namespace Abilities
{
    public class ItemAbilityContainer : AbilityContainer
    {
        IInventory _inventory;

        public override bool canBeUsed => _inventory.FindItemCount(_item) > 0;

        protected override IAbility ability => _ability;

        IAbility _ability { get; init; }
        IItem _item { get; init; }

        public ItemAbilityContainer(IItem item, IInventory inventory, IAbility ability)
        {
            _item = item;
            _inventory = inventory;
            _ability = ability;
        }

        public override void UseAbility(IAbilityTarget target)
        {
            _ability.Use(target);
            _inventory.RemoveOneItem(_item);
        }

        public override void UpdateAbilityCounter(IAbilityCounterHandler handler)
        {
            int numOfUses = _inventory.FindItemCount(_item);
            handler.ShowAbilityCounter(numOfUses);
        }

        public override bool TileHasValidTarget(ITileClickData tile)
        {
            return _ability.TileHasValidTarget(tile);
        }

    }
}