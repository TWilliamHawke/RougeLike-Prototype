using Core;
using UnityEngine;

namespace Items.Actions
{
    public class Equip : RadialActionFactory<ItemSlotData>
    {
        protected override IRadialMenuAction CreateAction(ItemSlotData itemSlot)
        {
            return new EquipAction(itemSlot);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlot)
        {
            return (itemSlot.slotContainer == ItemStorageType.inventory ||
                itemSlot.slotContainer == ItemStorageType.storage) &&
                itemSlot.item is IEquipment;
        }

        class EquipAction : IRadialMenuAction
        {
            public string actionTitle => "Equip";
            ItemSlotData _itemSlot;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.top;

            public EquipAction(ItemSlotData itemSlot)
            {
                _itemSlot = itemSlot;
            }

            public void DoAction()
            {
                Debug.Log("Buy");
            }
        }
    }
}