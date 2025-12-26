using System.Linq;
using Core;
using Items.Equipment;
using UnityEngine;

namespace Items.Actions
{
    public class Replace : ContextActionFactory<ItemSlotData>
    {
        IEquipmentSelectior _equipmentSelectior;
        InventoryIterator _iterator;

        public Replace(IEquipmentSelectior equipmentSelectior, InventoryIterator iterator)
        {
            _equipmentSelectior = equipmentSelectior;
            _iterator = iterator;
        }

        protected override ContextActionContainer CreateAction(ItemSlotData itemSlot)
        {
            return new ReplaceAction(itemSlot, _equipmentSelectior);
        }

        protected override bool ElementIsValid(ItemSlotData element)
        {
            return element.item is IEquipment
                && _iterator.GetMainItems()
                .Any(slot => slot.GetEquipmentSlot().index == element.GetEquipmentSlot().index);
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
                    _equipmentSelectior.ShowMainItems(slotTemplate);
                }
            }
        }
    }
}