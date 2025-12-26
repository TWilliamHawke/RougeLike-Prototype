using System.Collections.Generic;
using Core;
using Core.UI;
using Entities.PlayerScripts;
using Items.Actions;
using UnityEngine;

namespace Items.Equipment.UI
{
    public class EquipmentActionsController : ActionController<ItemSlotData>, IObserver<EquipmentSlot>
    {
		[SerializeField] EquipmentScreen _equipmentScreen;
        [SerializeField] PlayerEquipment _playerEquipment;
        [SerializeField] EquipmentSelectionScreen _equipmentSelectionScreen;
        [SerializeField] InventoryIterator _inventoryIterator;
        [Header("Item Actions")]
        [SerializeField] ContextActionTemplate _showInfo;
        [SerializeField] ContextActionTemplate _unequip;
        [SerializeField] ContextActionTemplate _replace;

        [InjectField] Player _player;

		List<ContextActionTemplate> _actions;

		void Awake()
		{
			_equipmentScreen.AddObserver(this);
			_actions = new()
			{
				_showInfo,
				_unequip,
				_replace
			};
		}

        public void AddToObserve(EquipmentSlot target)
        {
            target.OnSlotSelected += FillContextMenu;
        }

        public void RemoveFromObserve(EquipmentSlot target)
        {
            target.OnSlotSelected -= FillContextMenu;
        }

        protected override void FillFactory()
        {
            AddFactory(_showInfo, new ShowInfo<ItemSlotData>());
			AddFactory(_unequip, new Unequip(_playerEquipment, _player));
			AddFactory(_replace, new Replace(_equipmentSelectionScreen, _inventoryIterator));
        }

		private void FillContextMenu(ItemSlotData itemSlot)
		{
			FillContextMenu(itemSlot, _actions);
		}
    }
}