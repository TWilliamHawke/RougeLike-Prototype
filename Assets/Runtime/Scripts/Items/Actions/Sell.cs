using Core;
using UnityEngine;

namespace Items.Actions
{
    public class Sell : ContextActionFactory<ItemSlotData>
    {
        protected override ContextActionContainer CreateAction(ItemSlotData itemSlot)
        {
            return new SellAction(itemSlot);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlot)
        {
            return true;
        }

        class SellAction : ContextActionContainer
        {
            ItemSlotData _itemSlot;

            public SellAction(ItemSlotData itemSlot)
            {
                _itemSlot = itemSlot;
            }

            public override void DoAction()
            {
                Debug.Log("Buy");
            }
        }
    }
}