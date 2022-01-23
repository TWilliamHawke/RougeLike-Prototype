using System.Collections;
using System.Collections.Generic;
using Magic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "SpellTome", menuName = "Items/Spell Tome")]
    public class SpellTome : Item
    {
        [SpritePreview]
        [SerializeField] Spell _spell;

        public override int value => base.value * (_spell.startRank + 1);
    }
}