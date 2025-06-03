using Items;
using Magic;

namespace Abilities
{
    public interface IAbilitiesFactory
    {
        IAbilityContainer CreateItemAbility(Item item, IAbility ability);
        IAbilityContainer CreateSpellAbility(KnownSpellData spellData);
    }
}