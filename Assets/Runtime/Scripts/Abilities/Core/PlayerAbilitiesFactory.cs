using Items;
using Magic;
using UnityEngine;

namespace Abilities
{
    public class PlayerAbilitiesFactory : MonoBehaviour, IAbilitiesFactory
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] MagicConfig _magicConfig;

        IAbilityUser _abilityUser;

        public IAbilityUser abilityUser => _abilityUser;

        void Awake()
        {
            _abilityUser = GetComponent<AbilityController>();
        }

        public IAbilityContainer CreateItemAbilityContainer(Item item, IAbility ability)
        {
            return new ItemAbilityContainer(item, _inventory, ability);
        }

        public SpellContainer CreateSpellAbilityContainer(KnownSpellData spell)
        {
            return new SpellContainer(spell, abilityUser, _magicConfig);
        }
    }
}