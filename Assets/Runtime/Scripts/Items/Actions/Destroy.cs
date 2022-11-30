using Core;
using UnityEngine;

namespace Items.Actions
{
    public class Destroy : IItemAction
    {
        public string actionTitle => "Destroy";
        Inventory _inventory;
        public IItemSlot itemSlot { get; set; }

        public RadialButtonPosition preferedPosition => RadialButtonPosition.bottomLeft;

        public Destroy(IItemSlot itemSlot, Inventory inventory = null)
        {
            this.itemSlot = itemSlot;
            _inventory = inventory;
        }

        public Destroy(Inventory inventory)
        {
            _inventory = inventory;
        }

        // public void DoAction()
        // {
        //     (itemSlot?.itemSlotData?.item as IDestroyable)?.AddItemComponentsTo(_inventory);
        // }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return (itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage) &&
                itemSlot.itemSlotData.item is IDestroyable;

        }
    }
}