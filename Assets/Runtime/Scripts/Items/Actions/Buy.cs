using Core;
using UnityEngine;

namespace Items.Actions
{
    public class Buy : RadialActionFactory<ItemSlotData>
    {
        protected override IRadialMenuAction CreateAction(ItemSlotData itemSlotData)
        {
            return new BuyAction(itemSlotData);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlotitemSlotData)
        {
            return itemSlotitemSlotData.slotContainer == ItemStorageType.trader;
        }

        class BuyAction : IRadialMenuAction
        {
            public string actionTitle => "Buy";
            public RadialButtonPosition preferedPosition => RadialButtonPosition.top;
            
            ItemSlotData _itemSlotData;

            public BuyAction(ItemSlotData itemSlotData)
            {
                _itemSlotData = itemSlotData;
            }

            public void DoAction()
            {
                Debug.Log("Buy");
            }
        }
    }
}