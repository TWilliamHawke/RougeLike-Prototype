using Core;
using UnityEngine;

namespace Items.Actions
{
    public class MoveToStorage : RadialActionFactory<ItemSlotData>
    {
        protected override IRadialMenuAction CreateAction(ItemSlotData itemSlot)
        {
            return new MoveToStorageAction(itemSlot);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlot)
        {
            return itemSlot.slotContainer == ItemStorageType.inventory;
        }

        class MoveToStorageAction : IRadialMenuAction
        {
            public string actionTitle => "MoveToStorage";
            ItemSlotData _itemSlot;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.bottom;

            public MoveToStorageAction(ItemSlotData itemSlot)
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