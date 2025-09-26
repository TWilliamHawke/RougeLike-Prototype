using Items;
using Magic;

namespace Abilities
{
    public interface IAbilitiesFactory
    {
        IAbilityContainer CreateItemContainer(ItemTemplate item, IAbility ability);
        SpellContainer CreateSpellContainer(KnownSpellData spellData);
        IAbilityUser abilityUser { get; }
    }
}