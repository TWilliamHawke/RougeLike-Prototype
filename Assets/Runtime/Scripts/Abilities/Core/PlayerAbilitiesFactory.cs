using Entities;
using Items;
using Items.Equipment;
using Magic;
using UnityEngine;

namespace Abilities
{
    [RequireComponent(typeof(AbilityController))]
    public class PlayerAbilitiesFactory : MonoBehaviour, IAbilitiesFactory, IEntityComponent
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] PlayerEquipment _equipment;
        [SerializeField] MagicConfig _magicConfig;

        AbilityController _abilityUser;

        void Awake()
        {
            _abilityUser = GetComponent<AbilityController>();
        }

        public IAbilityContainer CreateItemContainer(IItem item, IAbility ability)
        {
            ability.BindAbilityUser(_abilityUser);
            return new ItemAbilityContainer(item, _inventory, ability);
        }

        public SpellContainer CreateSpellContainer(KnownSpellData spell)
        {
            return new SpellContainer(spell, _abilityUser, _magicConfig);
        }

        public SimpleAbilityContainer CreateSimpleContainer(IAbility ability)
        {
            ability.BindAbilityUser(_abilityUser);
            return new SimpleAbilityContainer(ability);
        }

        public IAbilityContainer CreateEquipmentContainer(IEquipmentSlotTemplate slot, IAbility ability)
        {
            ability.BindAbilityUser(_abilityUser);
            return new EquipmentAbilityContainer(ability, _equipment, slot);
        }
    }
}