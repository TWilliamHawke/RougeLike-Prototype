using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Abilities
{
    public class ItemAbilityContainer : IAbilityContainer
    {
        IInventory _inventory;

        public Sprite icon => _item.icon;
        public bool canBeUsed => _inventory.FindItemCount(_item) > 0;
        public string displayName => _item.displayName;
        public int numOfUses => _inventory.FindItemCount(_item);

        Item _item;
        IAbility _ability;

        public ItemAbilityContainer(Item item, IInventory inventory, IAbility ability)
        {
            _item = item;
            _inventory = inventory;
            _ability = ability;
        }

        public void UseAbility(AbilityController controller)
        {
            _ability.Use(controller);
            _inventory.RemoveOneItem(_item);
            controller.PlaySound(_item.useSound);
        }
    }
}