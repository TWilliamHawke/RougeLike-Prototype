using Core;
using UnityEngine;

namespace Items.Actions
{
    public class Sell : RadialActionFactory<ItemSlotData>
    {
        protected override IRadialMenuAction CreateAction(ItemSlotData itemSlot)
        {
            return new SellAction(itemSlot);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlot)
        {
            return itemSlot.slotContainer != ItemStorageType.trader && false;
        }

        class SellAction : IRadialMenuAction
        {
            public string actionTitle => "Cell";
            ItemSlotData _itemSlot;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.bottom;

            public SellAction(ItemSlotData itemSlot)
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