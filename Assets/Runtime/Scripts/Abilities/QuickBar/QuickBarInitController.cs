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
            var abilitiesFactory = _palyer.GetComponent<PlayerAbilitiesFactory>();
            var movementAbility = abilitiesFactory.CreateSimpleContainer(_movementAbility.CreateAbility());
            _spellbook.TryAddSpell(_defaultAbility);
            var spell = _spellbook.knownSpells.FirstOrDefault(spell => spell.SpellIsTheSame(_defaultAbility));
            var spellContainer = abilitiesFactory.CreateSpellContainer(spell);
            _quickBarData.SetMovementAbility(movementAbility);
            _quickBarData.SetMainAbility(spellContainer);
            _quickBarData.Init();
        }

        void OnDestroy()
        {
            _quickBarData.Reset();
        }
    }
}