using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Entities.PlayerScripts;
using UI.DragAndDrop;
using UnityEngine.Events;
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

        [InjectField] Player _player;

        KnownSpellData _knownSpell;
        SpellContainer _spellContainer;

        public bool waitForAllDependencies => false;
        //drag spell
        public KnownSpellData dragData => _knownSpell;
        public IDragController dataHandler => _dragDataHandler;
        public bool allowToDrag => _knownSpell is not null;

        DragController<KnownSpellData> _dragDataHandler;

        public event UnityAction<SpellContainer> OnDragStart;
        public event UnityAction<KnownSpellData> OnEditButtonClick;
        public event UnityAction<SpellContainer> OnSpellSelect;

        void Awake()
        {
            _dragDataHandler = new(this, _draggedSpellPrefab);
            var dragHandler = GetComponent<DragHandler>();
            dragHandler.OnDragStart += TriggerDragEvent;
        }

        public void OnPointerClick(PointerEventData _)
        {
            OnSpellSelect?.Invoke(_spellContainer);
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
            TryCreateSpellContainer();
        }

        public void TryCreateSpellContainer()
        {
            if (_knownSpell is null || _player is null) return;
            var abilityFactory = _player.GetComponent<PlayerAbilitiesFactory>();
            _spellContainer = abilityFactory.CreateSpellContainer(_knownSpell);
            _spellCost.text = _spellContainer.spellCost.ToString();
        }

        public void TriggerSpellEditEvent()
        {
            if (_knownSpell is null) return;
            OnEditButtonClick?.Invoke(_knownSpell);
        }

        private void TriggerDragEvent()
        {
            if (_knownSpell is null) return;
            OnDragStart?.Invoke(_spellContainer);
        }
    }
}