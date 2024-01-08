using System.Collections;
using System.Collections.Generic;
using Effects;
using Magic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "SpellTome", menuName = "Items/Spell Tome")]
    public class SpellTome : Item, IDestroyable, IUsableItem
    {
        [SpritePreview]
        [SerializeField] Spell _spell;

        [HideInInspector]
        [SerializeField] LootTable _resourcesData;
        [HideInInspector]
        [SerializeField] Spellbook _spellBook;

        public override int value => base.value * (_spell.startRank + 1);
        public Spell spell => _spell;
        public LootTable resourcesData => _resourcesData;
        public bool destroyAfterUse => !_spellBook.SpellIsKnown(_spell) || _spellHasBeenAdded;

        bool _spellHasBeenAdded = false;

        public override string GetDescription()
        {
            return _spell.displayName;
        }

        public override string GetItemType()
        {
            return "SpellTome";
        }

        public void UseItem(AbilityController abilityController)
        {
            _spellHasBeenAdded = _spellBook.TryAddSpell(_spell);
        }
    }
}