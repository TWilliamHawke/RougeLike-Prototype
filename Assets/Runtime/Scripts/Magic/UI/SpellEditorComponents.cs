using System.Collections;
using System.Collections.Generic;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Magic.UI
{
    public class SpellEditorComponents : MonoBehaviour
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] TextMeshProUGUI _spellCost;
        [SerializeField] TextMeshProUGUI _spellDescription;
        [SerializeField] TextMeshProUGUI _header;
        [SerializeField] HorizontalLayoutGroup _spellCostWrapper;
        [SerializeField] Button _closeButton;
        [SerializeField] Button _clearSlotButon;
        [SerializeField] SpellStringList _stringList;
        [SerializeField] Image _frame;

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
        }

        public void ShowSlotEffects(SpellString spellString)
        {
            HideAllComponents();
            SetHeaderText(spellString.displayName);
            SetDescriptionText(spellString.GetDescription());
        }

        public void ShowEmtySlotOptions()
        {
            HideAllComponents();
            SetHeaderText("Select new Spell String");
            _stringList.Show();
            _stringList.UpdateLayout(_inventory.spellStrings);
        }

        public void ShowSpellLineEffect(KnownSpellData spellData, SpellString spellString)
        {
            HideAllComponents();
            SetHeaderText("Add new Spell String");
            SetSpellCost(spellData.manaCost);
            SetDescriptionText(spellData.ConstructDescription());
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
            _clearSlotButon.Hide();
            _closeButton.Hide();
            _stringList.Hide();
            _frame.Hide();
        }
    }
}
