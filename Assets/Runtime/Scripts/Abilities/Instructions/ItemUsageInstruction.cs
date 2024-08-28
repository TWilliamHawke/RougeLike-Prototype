using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Abilities
{
	public class ItemUsageInstruction : IAbilityContainer
	{
		Inventory _inventory;

        public Sprite abilityIcon => _item.icon;
        public bool canBeUsed => _inventory.FindItemCount(_item) > 0;
        public string displayName => _item.displayName;
        public int numOfUses => _inventory.FindItemCount(_item);

        Item _item;

        public ItemUsageInstruction(Item item, Inventory inventory)
        {
            _item = item;
            _inventory = inventory;
        }

        public void UseAbility(AbilityController controller)
        {
            (_item as IItemWithAbility)?.UseAbility(controller);
            controller.PlaySound(_item.useSound);
        }
    }
}