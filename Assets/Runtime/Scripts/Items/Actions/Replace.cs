using Core;
using Entities;
using Entities.PlayerScripts;
using Items.Equipment;
using UnityEngine;

namespace Items.Actions
{
    public class Replace : Unequip
    {
        public Replace(IEquipmentController equipmentController, Player player) : base(equipmentController, player)
        {
            
        }

        protected override ContextActionContainer CreateAction(ItemSlotData itemSlot)
        {
            return new ReplaceAction(itemSlot, _equipmentController, _soundController);
        }

        class ReplaceAction : UnequipAction
        {
            public ReplaceAction(ItemSlotData itemSlot, IEquipmentController equipmentController, AudioEffectsController soundController) : base(itemSlot, equipmentController, soundController)
            {
                
            }

            public override void DoAction()
            {
                base.DoAction();
                Debug.Log("Replace");
            }
        }
    }
}