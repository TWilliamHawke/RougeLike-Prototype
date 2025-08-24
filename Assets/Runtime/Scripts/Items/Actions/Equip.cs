using Core;
using UnityEngine;

namespace Items.Actions
{
    public class Equip : ContextActionFactory<ItemSlotData>
    {
        protected override ContextActionContainer CreateAction(ItemSlotData itemSlot)
        {
            return new EquipAction(itemSlot);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlot)
        {
            return itemSlot.item is IEquipment;
        }

        class EquipAction : ContextActionContainer
        {
            ItemSlotData _itemSlot;

            public EquipAction(ItemSlotData itemSlot)
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