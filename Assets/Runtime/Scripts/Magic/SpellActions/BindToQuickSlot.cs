using Abilities;
using Core;
using Core.UI;

namespace Magic.Actions
{
    public class BindToQuickSlot : BindToQuickbar<KnownSpellData>
    {
        public BindToQuickSlot(PlayerAbilitiesFactory abilitiesFactory, QuickBarSetupController quickBarSetupController) : base(abilitiesFactory, quickBarSetupController)
        {
        }

        protected override ContextActionContainer CreateAction(KnownSpellData element)
        {
            return CreateAction(element);
        }

        protected override bool ElementIsValid(KnownSpellData element)
        {
            return true;
        }
    }
}
