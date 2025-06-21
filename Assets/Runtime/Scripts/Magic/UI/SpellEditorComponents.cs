using System.Collections;
using System.Collections.Generic;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Magic.UI
{
    public class SpellEditorComponents : MonoBehaviour
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] ItemSectionTemplate _spellStringSection;
        [SerializeField] Spellbook _spellbook;
        [SerializeField] TextMeshProUGUI _spellCost;
        [SerializeField] TextMeshProUGUI _spellDescription;
        [SerializeField] TextMeshProUGUI _header;
        [SerializeField] HorizontalLayoutGroup _spellCostWrapper;
        [SerializeField] SpellStringList _stringList;
        [SerializeField] Image _frame;
        [SerializeField] SpellEditorButtons _buttons;

        public void ShowDefaultEffects(SpellContainer spell)
        {
            HideAllComponents();
            SetSpellCost(spell.spellCost);
            SetDescriptionText(spell.ConstructDescription());
        }

        public void ShowRankUpEffects(SpellContainer spellContainer)
        {
            HideAllComponents();
            SetHeaderText("Upgrade Spell");
            SetSpellCost(spellContainer.GetRankUpSpellCost());
            SetDescriptionText(spellContainer.GetRankUpDescription());
            _buttons.ShowRankUpButton(_spellbook.increaseRankCost);
        }

        public void ShowSlotEffects(StringSlotData spellString)
        {
            HideAllComponents();
            SetHeaderText(spellString.displayName);
            SetDescriptionText(spellString.GetDescription());
            _buttons.ShowClearButton(_spellbook.clearSlotCost);
            _buttons.ShowCloseButton();
        }

        public void ShowEmptySlotOptions()
        {
            HideAllComponents();
            SetHeaderText("Select new Spell String");
            _stringList.Show();
            _stringList.UpdateLayout(_inventory.GetSection(_spellStringSection));
            _buttons.ShowCloseButton();
        }

        public void ShowSpellLineEffect(SpellContainer spellContainer, SpellString spellString, int slotIndex)
        {
            HideAllComponents();
            SetHeaderText(spellString.displayName);
            SetSpellCost(spellContainer.GetSpellCostWith(slotIndex, spellString));
            SetDescriptionText(spellContainer.ConstructDescriptionWith(slotIndex,spellString));
            _buttons.ShowConfirmButton();
        }

        private void SetHeaderText(string text)
        {
            _frame.Show();
            _header.Show();
            _header.text = text;
        }

        private void SetDescriptionText(string text)
        {
            _spellDescription.Show();
            _spellDescription.text = text;
        }

        private void SetSpellCost(string cost)
        {
            _spellCostWrapper.Show();
            _spellCost.text = cost.ToString();
        }

        private void SetSpellCost(int cost)
        {
            SetSpellCost(cost.ToString());
        }

        private void HideAllComponents()
        {
            _spellCostWrapper.Hide();
            _spellDescription.Hide();
            _header.Hide();
            _stringList.Hide();
            _frame.Hide();
            _buttons.HideAll();
        }
    }
}
