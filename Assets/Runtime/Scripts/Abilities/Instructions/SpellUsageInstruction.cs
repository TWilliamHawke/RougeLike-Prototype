using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Magic;
using Entities;
using Entities.Stats;
using System;

namespace Abilities
{
    public class SpellUsageInstruction : IAbilityContainer
    {
        ISafeStatController _manaStorage;
        KnownSpellData _spellData;

        public Sprite abilityIcon => _spellData.icon;
        public bool canBeUsed => _manaStorage.currentValue >= _spellData.manaCost;
        public string displayName => _spellData.displayName;
        public int numOfUses => -1;

        public SpellUsageInstruction(KnownSpellData spellData, ISafeStatController manaStorage)
        {
            _spellData = spellData;
            _manaStorage = manaStorage;
        }

        public void UseAbility(AbilityController controller)
        {
            if (_manaStorage.TryReduceStat(_spellData.manaCost))
            {
                _spellData.spellEffect.SelectControllerUsage(controller);
            }
        }

        public bool HasSpell(KnownSpellData spell)
        {
            return _spellData == spell;
        }
    }
}