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

        public override int value => base.value * (_spell.startRank + 1);

        public void AddItemComponentsTo(Inventory inventory)
        {
            inventory.resources.AddResource(ResourceType.magicDust, _spell.startRank * 50);
        }
    }
}