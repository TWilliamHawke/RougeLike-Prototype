using Core;

namespace Items.Actions
{
    public class Equip : IItemAction
    {
        public string actionTitle => "Equip";
        public IItemSlot itemSlot { get; set; }

        public RadialButtonPosition preferedPosition => RadialButtonPosition.top;

        public Equip(IItemSlot itemSlot)
        {
            this.itemSlot = itemSlot;
        }

        public Equip()
        {
        }

        // public void DoAction()
        // {

        // }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return (itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage) &&
                itemSlot.itemSlotData.item is IEquipment;
        }
    }
}