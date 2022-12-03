using System.Collections;
using System.Collections.Generic;
using Magic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "SpellTome", menuName = "Items/Spell Tome")]
    public class SpellTome : Item, IDestroyable
    {
        [SpritePreview]
        [SerializeField] Spell _spell;

        [SerializeField] Spellbook _spellbook;
        [SerializeField] Resource _magicDustResource;

        const int DUST_PER_SPELL_RANK = 50;

        public override int value => base.value * (_spell.startRank + 1);

        public void AddItemComponentsTo(Inventory inventory)
        {
            inventory.resources.AddResource(ResourceType.magicDust, _spell.startRank * DUST_PER_SPELL_RANK);
        }

        public override string GetDescription()
        {
            return _spell.displayName;
        }

        public override string GetItemType()
        {
            return "SpellTome";
        }

        public void AddItemComponentsTo(ref List<ItemSlotData> items)
        {
            items.Add(new ItemSlotData(_magicDustResource, _spell.startRank * DUST_PER_SPELL_RANK));
        }
    }
}