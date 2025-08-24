using Abilities;
using Core;
using Core.UI;

namespace Items.Actions
{
    public class UseAbility : UseAbility<ItemSlotData>
    {
        public UseAbility(IAbilitiesFactory abilitiesFactory, AbilityController abilityController) : base(abilitiesFactory, abilityController)
        {
        }

        protected override ContextActionContainer CreateAction(ItemSlotData itemSlot)
        {
            var item = itemSlot.item as IAbilitySource;
            return CreateAction(item);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlot)
        {
            return itemSlot?.item is IAbilitySource;
        }
    }
}