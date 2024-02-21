using System.Collections;
using System.Collections.Generic;
using Effects;
using Items;
using UnityEngine;

namespace Magic
{
    public struct StringSlotData : IEffectSource
    {
        public SpellString spellString { get; set; }
        public int slotIndex { get; init; }

        public string displayName => spellString.displayName;

        public StringSlotData(SpellString spellString, int slotIndex)
        {
            this.spellString = spellString;
            this.slotIndex = slotIndex;
        }

        public string GetDescription()
        {
            return spellString.GetDescription();
        }

        public bool IsEmpty()
        {
            return spellString == null;
        }

        public void Clear()
        {
            spellString = null;
        }
    }
}
