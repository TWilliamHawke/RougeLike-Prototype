using Items;
using Magic;

namespace Abilities
{
    public interface IAbilitiesFactory
    {
        IAbilityContainer CreateItemContainer(Item item, IAbility ability);
        SpellContainer CreateSpellContainer(KnownSpellData spellData);
        IAbilityUser abilityUser { get; }
    }
}