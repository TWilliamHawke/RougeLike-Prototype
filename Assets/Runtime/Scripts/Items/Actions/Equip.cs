using Core;

namespace Items.Actions
{
    public class Equip : ItemActionsFactory
    {
        protected override IRadialMenuAction CreateAction(IItemSlot itemSlot)
        {
            return new EquipAction(itemSlot);
        }

        protected override bool SlotIsValid(IItemSlot itemSlot)
        {
            return (itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage) &&
                itemSlot.itemSlotData.item is IEquipment;
        }

        class EquipAction : IItemAction
        {
            public string actionTitle => "Equip";
            IItemSlot _itemSlot;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.top;

            public EquipAction(IItemSlot itemSlot)
            {
                _itemSlot = itemSlot;
            }
        }
    }
}