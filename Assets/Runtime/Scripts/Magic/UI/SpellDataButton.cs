using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Magic.UI
{
    public class SpellDataButton : UIDataElement<KnownSpellData>, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] Color _defaultColor = Color.red;
        [SerializeField] Color _hoveredColor = Color.red;
        [SerializeField] Spellbook _spellBook;
        [Header("UI Elements")]
        [SerializeField] Image _frame;
        [SerializeField] Image _spellIcon;
        [SerializeField] TextMeshProUGUI _spellName;
        [SerializeField] TextMeshProUGUI _spellRank;
        [SerializeField] TextMeshProUGUI _spellCost;
        [SerializeField] Button _spellUpgradeButton;

        KnownSpellData _knownSpell;

        void Awake()
        {
            _spellUpgradeButton.onClick.AddListener(OpenSpellPage);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Select spell");
        }

        public void OnPointerEnter(PointerEventData _)
        {
            _frame.color = _hoveredColor;
        }

        public void OnPointerExit(PointerEventData _)
        {
            _frame.color = _defaultColor;
        }

        public override void UpdateData(KnownSpellData data)
        {
            _knownSpell = data;
            _frame.color = _defaultColor;
            _spellIcon.sprite = data.icon;

            _spellName.text = data.displayName;
            _spellRank.text = "Rank: " + data.rank.ToString();
            _spellCost.text = data.manaCost.ToString();
        }

        void OpenSpellPage()
        {
            _frame.color = _defaultColor;
            _spellBook.OpenSpellPage(_knownSpell);
        }



    }
}