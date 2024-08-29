using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Entities.PlayerScripts;
using UI.DragAndDrop;
using UnityEngine.Events;
using Core.UI;
using Abilities;

namespace Magic.UI
{
    [RequireComponent(typeof(DragHandler))]
    public class KnownSpellSlot : UIDataElement<KnownSpellData>, IPointerEnterHandler,
        IPointerExitHandler, IPointerClickHandler, IDragDataSource<KnownSpellData>, IInjectionTarget
    {
        [SerializeField] Color _defaultColor = Color.red;
        [SerializeField] Color _hoveredColor = Color.red;
        [SerializeField] DraggedSpell _draggedSpellPrefab;
        [Header("UI Elements")]
        [SerializeField] Image _frame;
        [SerializeField] Image _spellIcon;
        [SerializeField] TextMeshProUGUI _spellName;
        [SerializeField] TextMeshProUGUI _spellRank;
        [SerializeField] TextMeshProUGUI _spellCost;
        [Header("Events")]
        [SerializeField] CustomEvent _onSpellDragStart;
        [SerializeField] CustomEvent _onSpellDragEnd;

        KnownSpellData _knownSpell;

        public bool waitForAllDependencies => false;
        //drag spell
        public KnownSpellData dragData => _knownSpell;
        public IDragController dataHandler => _dragDataHandler;
        public bool allowToDrag => _knownSpell is not null;

        DragController<KnownSpellData> _dragDataHandler;

        public event UnityAction<KnownSpellData> OnDragStart;
        public event UnityAction<KnownSpellData> OnEditButtonClick;
        public event UnityAction<KnownSpellData> OnSpellSelect;

        void Awake()
        {
            _dragDataHandler = new(this, _draggedSpellPrefab);
            var dragHandler = GetComponent<DragHandler>();
            dragHandler.OnDragStart += TriggerDragEvent;
        }

        public void OnPointerClick(PointerEventData _)
        {
            OnSpellSelect?.Invoke(_knownSpell);
        }

        public void OnPointerEnter(PointerEventData _)
        {
            _frame.color = _hoveredColor;
        }

        public void OnPointerExit(PointerEventData _)
        {
            _frame.color = _defaultColor;
        }

        public override void BindData(KnownSpellData data)
        {
            _knownSpell = data;
            _frame.color = _defaultColor;
            _spellIcon.sprite = data.icon;

            _spellName.text = data.displayName;
            _spellRank.text = "Rank: " + data.rank.ToString();
            _spellCost.text = data.manaCost.ToString();
        }

        public void TriggerSpellEditEvent()
        {
            if (_knownSpell is null) return;
            OnEditButtonClick?.Invoke(_knownSpell);
        }

        private void TriggerDragEvent()
        {
            if (_knownSpell is null) return;
            OnDragStart?.Invoke(_knownSpell);
        }
    }
}