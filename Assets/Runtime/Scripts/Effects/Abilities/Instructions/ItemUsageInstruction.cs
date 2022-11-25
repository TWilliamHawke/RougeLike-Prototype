using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Effects
{
	public class ItemUsageInstruction : IAbilityInstruction
	{
		static Inventory _inventory;


        public Sprite abilityIcon => _item.icon;

        Item _item;
        

        public static void SetInventory(Inventory inventory)
        {
            if(_inventory != null) return;
            _inventory = inventory;
        }


        public ItemUsageInstruction(Item item)
        {
            _item = item;
        }

        //items count > 0
        public bool canBeUsed => _inventory.FindItemCount(_item) > 0;

        public void UseAbility(AbilityController controller)
        {
            (_item as IItemWithAbility)?.UseAbility(controller);
            controller.PlaySound(_item.useSound);
        }
    }
}