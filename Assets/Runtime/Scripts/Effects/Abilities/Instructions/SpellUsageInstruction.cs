using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Magic;
using Entities;

namespace Effects
{
	public class SpellContainer : IAbilityInstruction
	{
        static IManaComponent _manaComponent;

		KnownSpellData _spellData;


        public static void SetManaComponent(IManaComponent manaComponent)
        {
            if(_manaComponent != null) return; 
            _manaComponent = manaComponent;
        }

        public SpellContainer(KnownSpellData spellData)
        {
			_spellData = spellData;
        }

        public Sprite abilityIcon => _spellData.icon;

        public bool canBeUsed => _manaComponent.curentMana >= _spellData.manaCost;

        public void UseAbility(AbilityController controller)
        {
            if(_manaComponent.TrySpendMana(_spellData.manaCost))
            {
                _spellData.spell.SelectControllerUsage(controller);
            }
        }

	}
}