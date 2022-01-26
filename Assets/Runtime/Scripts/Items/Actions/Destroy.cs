using UnityEngine;

namespace Items.Actions
{
    public class Destroy : IItemAction
    {
        public string actionTitle => "Destroy";
        Inventory _inventory;

        public Destroy(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void DoAction(ItemSlotData itemSlotData)
        {
            (itemSlotData?.item as IDestroyable)?.AddItemComponentsTo(_inventory);
        }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return (itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage) &&
                itemSlot.itemSlotData.item is IDestroyable;

        }
    }
}