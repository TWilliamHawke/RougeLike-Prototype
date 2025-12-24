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

        IAbilityUser _user { get; init; }
        IAbility _ability { get; init; }
        ItemSlotData _itemSlot { get; init; }
        bool _canBeUsed { get; set; }

        public EquipmentAbilityContainer(IAbility ability, IAbilityUser user, IEquipmentStorage storage, IEquipmentSlotTemplate slot)
        {
            _ability = ability;
            _user = user;
            _itemSlot = storage.GetEquipment(slot);
            _canBeUsed = _itemSlot.count > 0;
            _itemSlot.OnSlotDataChanged += CheckItemSlot;
        }

        public override void UseAbility(IAbilityTarget target)
        {
            _ability.Use(_user, target);
        }

        public override void UpdateAbilityCounter(IAbilityCounterHandler handler)
        {
            handler.HideAbilityCounter();
        }

        public override bool TileHasValidTarget(ITileClickData tile)
        {
            return _ability.TileHasValidTarget(_user, tile);
        }

        private void CheckItemSlot()
        {
            _canBeUsed = _itemSlot.count > 0;
        }
    }
}