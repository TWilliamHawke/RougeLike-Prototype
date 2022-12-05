using Core;

namespace Items.Actions
{
    public class Equip : ItemActionsFactory
    {
        protected override IRadialMenuAction CreateAction(ItemSlotData itemSlot)
        {
            return new EquipAction(itemSlot);
        }

        protected override bool SlotIsValid(ItemSlotData itemSlot)
        {
            return (itemSlot.slotContainer == ItemContainerType.inventory ||
                itemSlot.slotContainer == ItemContainerType.storage) &&
                itemSlot.item is IEquipment;
        }

        class EquipAction : IItemAction
        {
            public string actionTitle => "Equip";
            ItemSlotData _itemSlot;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.top;

            public EquipAction(ItemSlotData itemSlot)
            {
                _itemSlot = itemSlot;
            }
        }
    }
}