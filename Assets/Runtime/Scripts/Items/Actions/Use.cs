using Core;
using Abilities;
using Entities;

namespace Items.Actions
{
    //requires for spell tomes etc
    public class Use : ContextActionFactory<ItemSlotData>
    {
        AudioEffectsController _soundController;

        public Use(AbilityController player)
        {
            _soundController = player
                .GetEntityComponent<AudioEffectsController>();
        }

        protected override ContextActionContainer CreateAction(ItemSlotData itemSlot)
        {
            return new UseAction(itemSlot, _soundController);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlot)
        {
            return itemSlot?.item is IUsableItem;
        }

        class UseAction : ContextActionContainer
        {
            ItemSlotData _itemSlot;
            IUsableItem _item;
            AudioEffectsController _soundController;

            public UseAction(ItemSlotData itemSlot, AudioEffectsController player)
            {
                _itemSlot = itemSlot;
                _item = itemSlot?.item as IUsableItem;
                _soundController = player;
            }

            public override void DoAction()
            {
                _item.Use();
                _soundController.PlaySound(_item.useSound);

                if (_item.destroyAfterUse)
                {
                    _itemSlot.RemoveOneItem();
                }
            }

        }
    }
}