using Core;
using UnityEngine;

namespace Items.Actions
{
    public class Buy : ContextActionFactory<ItemSlotData>
    {
        protected override ContextActionContainer CreateAction(ItemSlotData itemSlotData)
        {
            return new BuyAction(itemSlotData);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlotitemSlotData)
        {
            return true;
        }

        class BuyAction : ContextActionContainer
        {            
            ItemSlotData _itemSlotData;

            public BuyAction(ItemSlotData itemSlotData)
            {
                _itemSlotData = itemSlotData;
            }

            public override void DoAction()
            {
                Debug.Log("Buy");
            }
        }
    }
}