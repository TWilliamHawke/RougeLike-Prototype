using System.Collections.Generic;
using Abilities;
using Effects;
using Items;

namespace Magic
{
    public class ActiveStrings : IEffectsIterator
    {
        StringSlotData[] _activeStrings = new StringSlotData[6];
        EffectContainer _effectContainer = new();

        public bool StringSlotIsEmpty(int idx)
        {
            if (!IndexIsCorrect(idx)) return false;
            return _activeStrings[idx].IsEmpty();
        }

        public void SetActiveString(int slotIndex, SpellStringTemplate spellString)
        {
            if (!IndexIsCorrect(slotIndex)) return;
            StringSlotData slot = new(spellString, slotIndex);
            if (_activeStrings[slotIndex].spellString is not null)
            {
                _effectContainer.RemoveEffect(_activeStrings[slotIndex]);
            }
            _activeStrings[slotIndex] = slot;
            slot.ForEach(effect => _effectContainer.AddEffect(slot, effect));
        }

        public void ClearStringSlot(int idx, Inventory inventory)
        {
            if (StringSlotIsEmpty(idx)) return;
            inventory.AddItems(_activeStrings[idx].spellString, 1);
            _effectContainer.RemoveEffect(_activeStrings[idx]);
            _activeStrings[idx].Clear();
        }

        public void ClearAllSlots(Inventory inventory)
        {
            for (int i = 0; i < _activeStrings.Length; i++)
            {
                if (StringSlotIsEmpty(i)) continue;
                ClearStringSlot(i, inventory);
            }
        }

        public ActiveStrings Clone()
        {
            var clone = new ActiveStrings();
            for (int i = 0; i < _activeStrings.Length; i++)
            {
                if (StringSlotIsEmpty(i)) continue;
                clone.SetActiveString(i, _activeStrings[i].spellString);
            }

            return clone;
        }

        public IEnumerable<IStaticEffectData> GetEffects(IEffectSignature type)
        {
            return _effectContainer.GetEffects(type);
        }

        public IEnumerable<IStaticEffectData> GetEffects()
        {
            return _effectContainer.GetEffects();
        }

        public StringSlotData GetSpellSlotAt(int slotIndex)
        {
            if (!IndexIsCorrect(slotIndex)) return default;
            return _activeStrings[slotIndex];
        }

        private bool IndexIsCorrect(int idx)
        {
            return idx < _activeStrings.Length && idx >= 0;
        }

    }
}