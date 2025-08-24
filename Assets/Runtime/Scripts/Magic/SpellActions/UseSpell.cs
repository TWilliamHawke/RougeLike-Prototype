using Abilities;
using Core;
using Core.UI;

namespace Magic.Actions
{
    public class UseSpell : UseAbility<KnownSpellData>
    {
        public UseSpell(IAbilitiesFactory abilitiesFactory, AbilityController abilityController) : base(abilitiesFactory, abilityController)
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
