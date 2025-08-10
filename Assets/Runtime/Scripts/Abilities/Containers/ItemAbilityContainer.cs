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

        Item _item;

        public ItemAbilityContainer(Item item, IInventory inventory, IAbility ability)
        {
            _item = item;
            _inventory = inventory;
            _ability = ability;
        }

        public override void UseAbility(AbilityController controller)
        {
            _ability.UseBy(controller);
            _inventory.RemoveOneItem(_item);
            controller.PlaySound(_item.useSound);
        }

        public override void UpdateAbilityButton(IAbilityCounterHandler handler)
        {
            int numOfUses = _inventory.FindItemCount(_item);
            handler.ShowAbilityCounter(numOfUses);
        }
    }
}