using UnityEngine;
using Map;
using Items.Equipment;
using Items;

namespace Abilities
{
    public class EquipmentAbilityContainer : AbilityContainer
    {
        //UNDONE item is equipped
        public override bool canBeUsed => _canBeUsed; 
        protected override IAbility ability => _ability;

        IAbility _ability { get; init; }
        ItemSlotData _itemSlot { get; init; }
        bool _canBeUsed { get; set; }

        public EquipmentAbilityContainer(IAbility ability, IEquipmentStorage storage, IEquipmentSlotTemplate slot)
        {
            _ability = ability;
            _itemSlot = storage.GetEquipment(slot);
            _canBeUsed = _itemSlot.count > 0;
            _itemSlot.OnSlotDataChanged += CheckItemSlot;
        }

        public override void UseAbility(IAbilityTarget target)
        {
            _ability.Use(target);
        }

        public override void UpdateAbilityCounter(IAbilityCounterHandler handler)
        {
            handler.HideAbilityCounter();
        }

        public override bool TileHasValidTarget(ITileClickData tile)
        {
            return _ability.TileHasValidTarget(tile);
        }

        private void CheckItemSlot()
        {
            _canBeUsed = _itemSlot.count > 0;
        }
    }
}