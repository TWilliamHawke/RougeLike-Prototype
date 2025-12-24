using Entities;
using Entities.PlayerScripts;
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

        AbilityController _player;

        public IAbilityUser abilityUser
        {
            get
            {
                if (_player is null) _player = GetComponent<AbilityController>();
                return _player;
            }
        }

        public IAbilityContainer CreateItemContainer(IItem item, IAbility ability)
        {
            return new ItemAbilityContainer(item, _inventory, ability, _player);
        }

        public SpellContainer CreateSpellContainer(KnownSpellData spell)
        {
            return new SpellContainer(spell, abilityUser, _magicConfig);
        }

        public SimpleAbilityContainer CreateSimpleContainer(IAbility ability)
        {
            return new SimpleAbilityContainer(ability, abilityUser);
        }

        public IAbilityContainer CreateEquipmentContainer(IEquipmentSlotTemplate slot, IAbility ability)
        {
            return new EquipmentAbilityContainer(ability, abilityUser, _equipment, slot);
        }
    }
}