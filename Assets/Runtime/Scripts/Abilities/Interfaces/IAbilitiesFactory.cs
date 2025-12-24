using Items;
using Items.Equipment;
using Magic;

namespace Abilities
{
    public interface IAbilitiesFactory
    {
        IAbilityContainer CreateItemContainer(IItem item, IAbility ability);
        IAbilityContainer CreateEquipmentContainer(IEquipmentSlotTemplate slot, IAbility ability);
        SpellContainer CreateSpellContainer(KnownSpellData spellData);
        IAbilityUser abilityUser { get; }
    }
}