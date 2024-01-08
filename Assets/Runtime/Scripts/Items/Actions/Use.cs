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
            return (itemSlot.slotContainer == ItemStorageType.inventory ||
                itemSlot.slotContainer == ItemStorageType.storage) &&
                itemSlot?.item is IUsableItem;
        }

        class UseAction : IRadialMenuAction
        {
            public string actionTitle => "Use";
            ItemSlotData _itemSlot;
            IUsableItem _item;
            AbilityController _playerAbilityController;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.top;

            public UseAction(ItemSlotData itemSlot, AbilityController playerAbilityController)
            {
                _itemSlot = itemSlot;
                _item = itemSlot?.item as IUsableItem;
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