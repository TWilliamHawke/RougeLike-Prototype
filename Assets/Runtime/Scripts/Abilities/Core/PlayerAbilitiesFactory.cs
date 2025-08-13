using Items;
using Magic;
using UnityEngine;

namespace Abilities
{
    public class PlayerAbilitiesFactory : MonoBehaviour, IAbilitiesFactory
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] MagicConfig _magicConfig;

        IAbilityUser _player;

        public IAbilityUser abilityUser
        {
            get
            {
                if (_player is null) _player = GetComponent<IAbilityUser>();
                return _player;
            }
        }

        public IAbilityContainer CreateItemContainer(Item item, IAbility ability)
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
    }
}