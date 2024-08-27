using System.Collections;
using System.Collections.Generic;
using Effects;
using Items;
using UnityEngine;

namespace Magic
{
    public struct StringSlotData : IEffectSource, IEnumerable<SourceEffectData>
    {
        public SpellString spellString { get; set; }
        public int slotIndex { get; init; }

        public string displayName => spellString.displayName;
        public Sprite icon => spellString.icon;

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

        public override string ToString()
        {
            return "Spell slot #" + slotIndex;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is StringSlotData)) return false;
            return Equals((StringSlotData)obj);
        }

        public static bool operator ==(StringSlotData a, StringSlotData b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(StringSlotData a, StringSlotData b)
        {
            return !a.Equals(b);
        }

        public override int GetHashCode()
        {
            return 125 + slotIndex;
        }

        private bool Equals(StringSlotData other)
        {
            return other.slotIndex == this.slotIndex;
        }

        public IEnumerator<SourceEffectData> GetEnumerator()
        {
            return spellString.effects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return spellString.effects.GetEnumerator();
        }
    }
}
