using Core;
using UnityEngine;

namespace Items.Actions
{
    public class MoveToStorage : ContextActionFactory<ItemSlotData>
    {
        protected override ContextActionContainer CreateAction(ItemSlotData itemSlot)
        {
            return new MoveToStorageAction(itemSlot);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlot)
        {
            return true;
        }

        class MoveToStorageAction : ContextActionContainer
        {
            ItemSlotData _itemSlot;

            public MoveToStorageAction(ItemSlotData itemSlot)
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