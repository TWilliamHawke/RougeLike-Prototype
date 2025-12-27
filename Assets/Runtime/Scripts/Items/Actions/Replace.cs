using Core;
using Items.Equipment;

namespace Items.Actions
{
    public class Replace : ContextActionFactory<ItemSlotData>
    {
        IEquipmentSelectior _equipmentSelectior;
        IInventoryIterator _iterator;

        public Replace(IEquipmentSelectior equipmentSelectior, IInventoryIterator iterator)
        {
            _equipmentSelectior = equipmentSelectior;
            _iterator = iterator;
        }

        protected override ContextActionContainer CreateAction(ItemSlotData itemSlot)
        {
            return new ReplaceAction(itemSlot, _equipmentSelectior, _iterator);
        }

        protected override bool ElementIsValid(ItemSlotData element)
        {
            return element.item is IEquipment
                && _iterator.HasEquipmentForSlot(element);
        }

        class ReplaceAction : ContextActionContainer
        {
            IEquipmentSelectior _equipmentSelectior;
            ItemSlotData _itemSlot;
            IInventoryIterator _iterator;

            public ReplaceAction(ItemSlotData itemSlot, IEquipmentSelectior equipmentSelectior, IInventoryIterator iterator)
            {
                _itemSlot = itemSlot;
                _equipmentSelectior = equipmentSelectior;
                _iterator = iterator;
            }

            public override void DoAction()
            {
                if (_itemSlot.item is IEquipment equipment)
                {
                    var slotTemplate = equipment.equipmentSlot;
                    _equipmentSelectior.ShowEquipmentInSection(slotTemplate, _iterator);
                }
            }
        }
    }
}