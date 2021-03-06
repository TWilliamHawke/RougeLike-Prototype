using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Entities.Player;
using UI.DragAndDrop;

namespace Magic.UI
{
    public class SpellDataButton : UIDataElement<KnownSpellData>, IPointerEnterHandler,
        IPointerExitHandler, IPointerClickHandler, IDragDataSource<KnownSpellData>
    {
        [SerializeField] Color _defaultColor = Color.red;
        [SerializeField] Color _hoveredColor = Color.red;
        [SerializeField] Spellbook _spellBook;
        [SerializeField] ActiveAbilities _activeAbilities;
        [SerializeField] DraggedSpell _draggedSpellPrefab;
        [SerializeField] DragController _dragController;
        [Header("UI Elements")]
        [SerializeField] Image _frame;
        [SerializeField] Image _spellIcon;
        [SerializeField] TextMeshProUGUI _spellName;
        [SerializeField] TextMeshProUGUI _spellRank;
        [SerializeField] TextMeshProUGUI _spellCost;
        [SerializeField] Button _spellUpgradeButton;

        KnownSpellData _knownSpell;
        DragHandler<KnownSpellData> _draghandler;

        public KnownSpellData dragData => _knownSpell;
        public DragableUIElement<KnownSpellData> dragableElementPrefab => _draggedSpellPrefab;
        public DragController dragController => _dragController;

        void Awake()
        {
            _spellUpgradeButton.onClick.AddListener(OpenSpellPage);
            _draghandler = new DragHandler<KnownSpellData>(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _spellBook.SelectSpell(_knownSpell);
            _activeAbilities.UseAbility(_knownSpell.spell);
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

        public void OnBeginDrag(PointerEventData eventData)
        {
            _draghandler.OnBeginDrag();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _draghandler.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _draghandler.OnEndDrag();
        }
    }
}