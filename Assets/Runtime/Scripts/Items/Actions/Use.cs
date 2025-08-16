using Core;
using Abilities;
using Entities;

namespace Items.Actions
{
    public class Use : RadialActionFactory<ItemSlotData>
    {
        AudioEffectsController _soundController;

        public Use(AbilityController player)
        {
            _soundController = player
                .GetEntityComponent<AudioEffectsController>();
        }

        protected override IRadialMenuAction CreateAction(ItemSlotData itemSlot)
        {
            return new UseAction(itemSlot, _soundController);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlot)
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
            AudioEffectsController _soundController;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.top;

            public UseAction(ItemSlotData itemSlot, AudioEffectsController player)
            {
                _itemSlot = itemSlot;
                _item = itemSlot?.item as IUsableItem;
                _soundController = player;
            }

            public void DoAction()
            {
                _item.Use();
                _soundController.PlaySound(_item.useSound);

                if(_item.destroyAfterUse)
                {
                    _itemSlot.RemoveOneItem();
                }
            }

        }
    }
}