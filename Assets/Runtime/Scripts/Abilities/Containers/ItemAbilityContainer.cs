using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Abilities
{
    public class ItemAbilityContainer : AbilityContainer
    {
        IInventory _inventory;

        public override bool canBeUsed => _inventory.FindItemCount(_item) > 0;

        protected override IAbility ability => _ability;
        IAbility _ability { get; init; }

        Item _item;

        public ItemAbilityContainer(Item item, IInventory inventory, IAbility ability)
        {
            _item = item;
            _inventory = inventory;
            _ability = ability;
        }

        public override void UseAbility(IAbilityTarget target)
        {
            _ability.UseOn(target);
            _inventory.RemoveOneItem(_item);
        }

        public override void UpdateAbilityButton(IAbilityCounterHandler handler)
        {
            int numOfUses = _inventory.FindItemCount(_item);
            handler.ShowAbilityCounter(numOfUses);
        }
    }
}