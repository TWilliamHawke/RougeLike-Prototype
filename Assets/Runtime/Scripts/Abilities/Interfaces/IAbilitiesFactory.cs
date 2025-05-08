using Items;
using Magic;

namespace Abilities
{
    public interface IAbilitiesFactory
    {
        IAbilityContainer CreateItemAbility(Item item);
        IAbilityContainer CreateSpellAbility(KnownSpellData spellData);
    }
}