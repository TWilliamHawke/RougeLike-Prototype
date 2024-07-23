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
        [SerializeField] Spellbook _spellbook;
        [SerializeField] TextMeshProUGUI _spellCost;
        [SerializeField] TextMeshProUGUI _spellDescription;
        [SerializeField] TextMeshProUGUI _header;
        [SerializeField] HorizontalLayoutGroup _spellCostWrapper;
        [SerializeField] SpellStringList _stringList;
        [SerializeField] Image _frame;
        [SerializeField] SpellEditorButtons _buttons;

        public void ShowDefaultEffects(KnownSpellData spellData)
        {
            HideAllComponents();
            SetSpellCost(spellData.manaCost);
            SetDescriptionText(spellData.ConstructDescription());
        }

        public void ShowRankUpEffects(KnownSpellData spellData)
        {
            HideAllComponents();
            SetHeaderText("Upgrade Spell");
            SetSpellCost(spellData.manaCost);
            SetDescriptionText(spellData.ConstructDescription());
            _buttons.ShowRankUpButton(_spellbook.increaseRankCost);
        }

        public void ShowSlotEffects(SpellString spellString)
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
            _stringList.UpdateLayout(_inventory.spellStrings);
            _buttons.ShowCloseButton();
        }

        public void ShowSpellLineEffect(KnownSpellData spellData, SpellString spellString)
        {
            HideAllComponents();
            SetHeaderText("Add new Spell String");
            SetSpellCost(spellData.manaCost);
            SetDescriptionText(spellData.ConstructDescription());
            _buttons.ShowConfirmButton();
        }

        void SetHeaderText(string text)
        {
            _frame.Show();
            _header.Show();
            _header.text = text;
        }

        void SetDescriptionText(string text)
        {
            _spellDescription.Show();
            _spellDescription.text = text;
        }

        void SetSpellCost(int cost)
        {
            _spellCostWrapper.Show();
            _spellCost.text = cost.ToString();
        }

        void HideAllComponents()
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
