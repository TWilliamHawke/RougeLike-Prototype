using Entities.PlayerScripts;
using Entities.Stats;
using Items;
using Magic;
using UnityEngine;

namespace Abilities
{
    public class AbilitiesFactory : MonoBehaviour
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

        public IAbilityContainer CreateItemAbility(Item item)
        {
            return new ItemUsageInstruction(item, _inventory);
        }

        public IAbilityContainer CreateSpellAbility(KnownSpellData spell)
        {
            return new SpellUsageInstruction(spell, _manaStorage);
        }
    }
}