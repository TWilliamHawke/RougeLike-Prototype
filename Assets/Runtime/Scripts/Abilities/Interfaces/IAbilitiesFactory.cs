using Items;
using Magic;

namespace Abilities
{
    public interface IAbilitiesFactory
    {
        IAbilityContainer CreateItemAbilityContainer(Item item, IAbility ability);
        SpellContainer CreateSpellAbilityContainer(KnownSpellData spellData);
    }
}