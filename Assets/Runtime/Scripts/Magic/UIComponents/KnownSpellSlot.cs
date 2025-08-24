using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Entities.PlayerScripts;
using UI.DragAndDrop;
using UnityEngine.Events;
using Abilities;
using Entities.Stats;

namespace Magic.UI
{
    public class KnownSpellSlot : UIDataElement<KnownSpellData>, IPointerEnterHandler,
        IPointerExitHandler, IPointerClickHandler, IInjectionTarget
    {
        [SerializeField] Color _defaultColor = Color.red;
        [SerializeField] Color _hoveredColor = Color.red;
        [SerializeField] DraggedSpell _draggedSpellPrefab;
        [SerializeField] MagicConfig _magicConfig;
        [Header("UI Elements")]
        [SerializeField] Image _frame;
        [SerializeField] Image _spellIcon;
        [SerializeField] TextMeshProUGUI _spellName;
        [SerializeField] TextMeshProUGUI _spellRank;
        [SerializeField] TextMeshProUGUI _spellCost;
        [SerializeField] CustomEvent _onSpellSelect;

        [InjectField] Player _player;

        KnownSpellData _knownSpell;

        public bool waitForAllDependencies => false;
        //drag spell
        public event UnityAction<KnownSpellData> OnEditButtonClick;
        public event UnityAction<KnownSpellData> OnSpellSelect;

        public void OnPointerClick(PointerEventData _)
        {
            OnSpellSelect?.Invoke(_knownSpell);
            _onSpellSelect.Invoke();
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
            TrySetSpellCost();
        }

        public void TrySetSpellCost()
        {
            if (_knownSpell is null || _player is null) return;
            var statsStorage = _player.GetEntityComponent<StatsStorage>();
            int spellCost = _magicConfig.GetSpellCost(_knownSpell, statsStorage);
            _spellCost.text = spellCost.ToString();
        }

        public void TriggerSpellEditEvent()
        {
            if (_knownSpell is null) return;
            OnEditButtonClick?.Invoke(_knownSpell);
        }
    }
}