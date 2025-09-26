using Items;
using Magic;

namespace Abilities
{
    public interface IAbilitiesFactory
    {
        IAbilityContainer CreateItemContainer(IItem item, IAbility ability);
        SpellContainer CreateSpellContainer(KnownSpellData spellData);
        IAbilityUser abilityUser { get; }
    }
}