using Abilities;
using Entities.PlayerScripts;
using Magic;
using Magic.UI;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(ComponentInjector))]
    public class KnownSpellCliickObserver : MonoBehaviour, IObserver<KnownSpellSlot>
    {
        [SerializeField] SpellList _spellList;
        [InjectField] Player _player;

        AbilityController _abilityController;
        AbilitiesFactory _abilityFactory;

        public void AddToObserve(KnownSpellSlot target)
        {
            target.OnSpellSelect += CastSpell;
        }

        public void RemoveFromObserve(KnownSpellSlot target)
        {
            target.OnSpellSelect -= CastSpell;
        }

        //Used in Unity Editor
        public void FindPlayerComponents()
        {
            _abilityController = _player.GetComponent<AbilityController>();
            _abilityFactory = _player.GetComponent<AbilitiesFactory>();
        }

        private void CastSpell(KnownSpellData spell)
        {
            var ability = spell.CreateAbilityContainer(_abilityFactory);
            ability.UseAbility(_abilityController);
        }
    }
}


