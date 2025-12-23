using Core;
using Entities;
using Entities.PlayerScripts;
using Items.Equipment;
using UnityEngine;

namespace Items.Actions
{
    public class Equip : ContextActionFactory<ItemSlotData>
    {
        IEquipmentController _equipmentController;
        AudioEffectsController _soundController;

        public Equip(IEquipmentController equipmentController, Player player)
        {
            _equipmentController = equipmentController;
            _soundController = player
                .GetEntityComponent<AudioEffectsController>();
        }

        protected override ContextActionContainer CreateAction(ItemSlotData itemSlot)
        {
            return new EquipAction(itemSlot, _equipmentController, _soundController);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlot)
        {
            return itemSlot.item is IEquipment;
        }

        class EquipAction : ContextActionContainer
        {
            ItemSlotData _itemSlot;
            IEquipmentController _equipmentController;
            AudioEffectsController _soundController;

            public EquipAction(ItemSlotData itemSlot, IEquipmentController equipmentController, AudioEffectsController soundController)
            {
                _itemSlot = itemSlot;
                _equipmentController = equipmentController;
                _soundController = soundController;
            }

            public override void DoAction()
            {
                _equipmentController.Equip(_itemSlot);
                _soundController.PlaySound(_itemSlot.item.dragSound);
            }
        }
    }
}