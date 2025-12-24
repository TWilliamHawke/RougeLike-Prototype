using Core;
using Entities;
using Entities.PlayerScripts;
using Items.Equipment;
using UnityEngine;

namespace Items.Actions
{
    public class Replace : ContextActionFactory<ItemSlotData>
    {
        IEquipmentSelectior _equipmentSelectior;

        public Replace(IEquipmentSelectior equipmentSelectior)
        {
            _equipmentSelectior = equipmentSelectior;
        }

        protected override ContextActionContainer CreateAction(ItemSlotData itemSlot)
        {
            return new ReplaceAction(itemSlot, _equipmentSelectior);
        }

        protected override bool ElementIsValid(ItemSlotData element)
        {
            return element.item is IEquipment;
        }

        class ReplaceAction : ContextActionContainer
        {
            IEquipmentSelectior _equipmentSelectior;
            ItemSlotData _itemSlot;

            public ReplaceAction(ItemSlotData itemSlot, IEquipmentSelectior equipmentSelectior)
            {
                _itemSlot = itemSlot;
                _equipmentSelectior = equipmentSelectior;
            }

            public override void DoAction()
            {
                if (_itemSlot.item is IEquipment equipment)
                {
                    var slotTemplate = equipment.equipmentSlot;
                }
                Debug.Log("Replace");
            }
        }
    }
}