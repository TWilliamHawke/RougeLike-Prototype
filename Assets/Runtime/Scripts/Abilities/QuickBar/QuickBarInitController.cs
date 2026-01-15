using System.Linq;
using Entities.PlayerScripts;
using Magic;
using UnityEngine;

namespace Abilities
{
    public class QuickBarInitController : MonoBehaviour
    {
        [SerializeField] QuickBarDataStorage _quickBarData;
        [SerializeField] Spellbook _spellbook;

        [SerializeField] MovementAbilityTemplate _movementAbility;
        [SerializeField] Spell _defaultAbility;

        [InjectField] Player _palyer;

        //Used in Unity Editor
        public void InitQuickBar()
        {
            var abilityController = _palyer.GetComponent<AbilityController>();
            var abilitiesFactory = _palyer.GetComponent<PlayerAbilitiesFactory>();

            CreateMovementAbility(abilitiesFactory, abilityController);
            CreateMainAbility(abilitiesFactory, abilityController);

            _quickBarData.Init();
        }

        private void CreateMainAbility(PlayerAbilitiesFactory abilitiesFactory, IAbilityUser abilityUser)
        {
            _spellbook.TryAddSpell(_defaultAbility);
            var spell = _spellbook.knownSpells.FirstOrDefault(spell => spell.SpellIsTheSame(_defaultAbility));
            var spellContainer = abilitiesFactory.CreateSpellContainer(spell);
            _quickBarData.SetMainAbility(spellContainer, abilityUser);
        }

        private void CreateMovementAbility(PlayerAbilitiesFactory abilitiesFactory, IAbilityUser abilityUser)
        {
            var ability = _movementAbility.CreateAbility();
            var container = abilitiesFactory.CreateSimpleContainer(ability);
            _quickBarData.SetMovementAbility(container, abilityUser);
        }

        void OnDestroy()
        {
            _quickBarData.Reset();
        }
    }
}