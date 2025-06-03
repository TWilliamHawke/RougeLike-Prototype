using Entities.PlayerScripts;
using Entities.Stats;
using Items;
using Magic;
using UnityEngine;

namespace Abilities
{
    public class PlayerAbilitiesFactory : MonoBehaviour, IAbilitiesFactory
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] StoredResource _mana;
        [SerializeField] PlayerStats _playerStats;

        ResourceStorage _manaStorage;

        // used in editor
        public void FindManaStorage()
        {
            _manaStorage = _playerStats.FindStorage(_mana);
        }

        public IAbilityContainer CreateItemAbility(Item item, IAbility ability)
        {
            return new ItemAbilityContainer(item, _inventory, ability);
        }

        public IAbilityContainer CreateSpellAbility(KnownSpellData spell)
        {
            return new SpellAbilityContainer(spell, _manaStorage);
        }
    }
}