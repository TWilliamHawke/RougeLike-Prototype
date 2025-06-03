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
        [SerializeField] MagicConfig _magicConfig;

        StatsContainer _statsContainer;

        void Awake()
        {
            _statsContainer = GetComponent<StatsContainer>();
        }

        // used in editor
        public void FindManaStorage()
        {

        }

        public IAbilityContainer CreateItemAbility(Item item, IAbility ability)
        {
            return new ItemAbilityContainer(item, _inventory, ability);
        }

        public IAbilityContainer CreateSpellAbility(KnownSpellData spell)
        {
            return new SpellContainer(spell, _statsContainer, _magicConfig);
        }
    }
}