using System.Collections.Generic;
using Core;
using Core.UI;
using Effects;
using System.Linq;

namespace Items.Actions
{
    public class Use : ItemActionsFactory
    {
        AbilityController _playerAbilityController;

        public Use(AbilityController playerAbilityController)
        {
            _playerAbilityController = playerAbilityController;
        }

        protected override IRadialMenuAction CreateAction(ItemSlotData itemSlot)
        {
            return new UseAction(itemSlot, _playerAbilityController);
        }

        protected override bool SlotIsValid(ItemSlotData itemSlot)
        {
            return (itemSlot.slotContainer == ItemContainerType.inventory ||
                itemSlot.slotContainer == ItemContainerType.storage) &&
                itemSlot?.item is IUsableInInventory;
        }

        class UseAction : IItemAction
        {
            public string actionTitle => "Use";
            ItemSlotData _itemSlot;
            IUsableInInventory _item;
            AbilityController _playerAbilityController;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.top;

            public UseAction(ItemSlotData itemSlot, AbilityController playerAbilityController)
            {
                _itemSlot = itemSlot;
                _item = itemSlot?.item as IUsableInInventory;
                _playerAbilityController = playerAbilityController;
            }

            public void DoAction()
            {
                _item.UseItem(_playerAbilityController);
                _playerAbilityController.PlaySound(_item.useSound);

                if(_item.destroyAfterUse)
                {
                    _itemSlot.RemoveOneItem();
                }
            }

        }
    }
}