using Core;
using Entities;
using Entities.PlayerScripts;
using Items.Equipment;

namespace Items.Actions
{
    public class Unequip : ContextActionFactory<ItemSlotData>
    {
        protected IEquipmentController _equipmentController;
        protected AudioEffectsController _soundController;

        public Unequip(IEquipmentController equipmentController, Player player)
        {
            _equipmentController = equipmentController;
            _soundController = player
                .GetEntityComponent<AudioEffectsController>();
        }

        protected override ContextActionContainer CreateAction(ItemSlotData itemSlot)
        {
            return new UnequipAction(itemSlot, _equipmentController, _soundController);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlot)
        {
            return itemSlot.item is IEquipment;
        }

        protected class UnequipAction : ContextActionContainer
        {
            ItemSlotData _itemSlot;
            IEquipmentController _equipmentController;
            AudioEffectsController _soundController;

            public UnequipAction(ItemSlotData itemSlot, IEquipmentController equipmentController, AudioEffectsController soundController)
            {
                _itemSlot = itemSlot;
                _equipmentController = equipmentController;
                _soundController = soundController;
            }

            public override void DoAction()
            {
                _equipmentController.Unequip(_itemSlot.GetEquipmentSlot());
                _soundController.PlaySound(_itemSlot.item.dragSound);
            }
        }
    }
}