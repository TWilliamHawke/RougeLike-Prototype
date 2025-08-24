using Abilities;
using Core;
using Core.UI;

namespace Items.Actions
{
    public class BindToQuickbar : BindToQuickbar<ItemSlotData>
    {
        public BindToQuickbar(PlayerAbilitiesFactory abilitiesFactory, QuickBarSetupController quickBarSetupController) : base(abilitiesFactory, quickBarSetupController)
        {
        }

        protected override ContextActionContainer CreateAction(ItemSlotData element)
        {
            var abilitySource = element.item as IAbilitySource;
            return CreateAction(abilitySource);
        }

        protected override bool ElementIsValid(ItemSlotData element)
        {
            return element.item is IAbilitySource;
        }
    }
}