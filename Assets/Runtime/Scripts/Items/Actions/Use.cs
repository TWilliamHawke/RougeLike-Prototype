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

        protected override IRadialMenuAction CreateAction(IItemSlot itemSlot)
        {
            return new UseAction(itemSlot, _playerAbilityController);
        }

        protected override bool SlotIsValid(IItemSlot itemSlot)
        {
            return (itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage) &&
                itemSlot?.itemSlotData?.item is IUsableInInventory;
        }

        class UseAction : IItemAction
        {
            public string actionTitle => "Use";
            IItemSlot _itemSlot;
            IUsableInInventory _item;
            AbilityController _playerAbilityController;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.top;

            public UseAction(IItemSlot itemSlot, AbilityController playerAbilityController)
            {
                _itemSlot = itemSlot;
                _item = itemSlot?.itemSlotData?.item as IUsableInInventory;
                _playerAbilityController = playerAbilityController;
            }

            public void DoAction()
            {
                _item.UseItem(_playerAbilityController);
                _playerAbilityController.PlaySound(_item.useSound);

                if(_item.destroyAfterUse)
                {
                    _itemSlot.itemSlotData.RemoveFromStack();
                }
            }

        }
    }
}