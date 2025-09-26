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
        ItemTemplate _item { get; init; }
        IAbilityUser _user { get; init; }

        public ItemAbilityContainer(ItemTemplate item, IInventory inventory, IAbility ability, IAbilityUser user)
        {
            _item = item;
            _inventory = inventory;
            _ability = ability;
            _user = user;
        }

        public override void UseAbility(IAbilityTarget target)
        {
            _ability.Use(_user, target);
            _inventory.RemoveOneItem(_item);
        }

        public override void UpdateAbilityButton(IAbilityCounterHandler handler)
        {
            int numOfUses = _inventory.FindItemCount(_item);
            handler.ShowAbilityCounter(numOfUses);
        }

        public override bool TileHasValidTarget(ITileClickData tile)
        {
            return _ability.TileHasValidTarget(_user, tile);
        }

    }
}