using Abilities;
using Entities.PlayerScripts;
using Magic;
using Magic.UI;
using UnityEngine;

namespace Items
{
    public class KnownSpellCliickObserver : MonoBehaviour, IObserver<KnownSpellSlot>
    {
        [SerializeField] SpellList _spellList;
        [InjectField] Player _player;

        void Awake()
        {
            _spellList.AddObserver(this);
        }

        public void AddToObserve(KnownSpellSlot target)
        {
            target.OnSpellSelect += CastSpell;
        }

        public void RemoveFromObserve(KnownSpellSlot target)
        {
            target.OnSpellSelect -= CastSpell;
        }

        private void CastSpell(KnownSpellData spell)
        {
            var abilitiesFactory = _player.GetComponent<PlayerAbilitiesFactory>();
            var container = spell.CreateAbilityContainer(abilitiesFactory);
            var controller = _player.GetComponent<AbilityController>();
            container.SelectBy(controller);
        }
    }
}


