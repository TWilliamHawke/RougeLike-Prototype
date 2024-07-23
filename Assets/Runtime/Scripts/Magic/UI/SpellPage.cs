using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Entities.PlayerScripts;
using Items;

namespace Magic.UI
{
    public class SpellPage : MonoBehaviour, IObserver<KnownSpellSlot>,
    IObserver<SpellStringPreview>
    {
        const int EMPTY_IDX = -2;
        const int RANK_UP_IDX = -1;

        [SerializeField] Inventory _inventory;
        [SerializeField] Spellbook _spellbook;
        [SerializeField] UIScreen _editorScreen;
        [Header("UI Elements")]
        [SerializeField] Image _spellIcon;
        [SerializeField] TextMeshProUGUI _spellName;
        [SerializeField] TextMeshProUGUI _spellRank;

        [SerializeField] ActiveStringSlot[] _spellStringSlots;
        [SerializeField] ActiveStringSlot _increaseRankButton;
        [SerializeField] SpellList _spellList;
        [SerializeField] SpellStringList _stringList;

        [SerializeField] SpellEditorComponents _editorComponents;
        [SerializeField] SpellEditorButtons _buttons;

        int _activeSlotIdx = EMPTY_IDX;
        KnownSpellData _spellData;
        SpellString _selectedString;

        public void Start()
        {
            _spellList.AddObserver(this);
            _stringList.AddObserver(this);
            _increaseRankButton.OnClick += OpenIncreaseRankPanel;
            _buttons.OnClearButtonClick += ClearSelectedSlot;
            _buttons.OnCloseButtonClick += ShowDefaultEffects;
            _buttons.OnConfirmButtonClick += HandleConfirmClick;

            for (int i = 0; i < _spellStringSlots.Length; i++)
            {
                ActiveStringSlot slot = _spellStringSlots[i];
                slot.Init(i);
                slot.OnClick += SelectSlot;
            }
        }

        public void Open(KnownSpellData spellData)
        {
            _spellData = spellData;
            spellData.OnDataChange += UpdateUIData;
            UpdateUIData();
            _editorScreen.Open();
            ShowDefaultEffects();
        }

        private void UpdateUIData()
        {
            _spellIcon.sprite = _spellData.icon;
            _spellName.text = _spellData.displayName;
            _spellRank.text = "Rank " + _spellData.rank;
            UpdateActiveSlots();
        }

        private void UpdateActiveSlots()
        {
            _increaseRankButton.gameObject.SetActive(!_spellData.spellHasMaxRank);
            for (int i = 0; i < _spellStringSlots.Length; i++)
            {
                var slot = _spellStringSlots[i];
                if (i >= _spellData.rank)
                {
                    slot.Disable();
                    continue;
                }

                slot.Enable();
                slot.SetIcon(_spellData.GetSpellStringAt(i));
            }
        }

        private void OpenIncreaseRankPanel(int _)
        {
            SelectSlot(-1);
        }

        private void SelectSlot(int idx)
        {
            UpdateSlotSelection(idx);
            if (idx == _activeSlotIdx)
            {
                ShowDefaultEffects();
                return;
            }

            if (idx == RANK_UP_IDX)
            {
                _editorComponents.ShowRankUpEffects(_spellData);
            }
            else if (_spellData.StringSlotIsEmpty(idx))
            {
                _editorComponents.ShowEmptySlotOptions();
            }
            else
            {
                _editorComponents.ShowSlotEffects(_spellData.GetSpellStringAt(idx));
            }

            _activeSlotIdx = idx;
        }

        private void UpdateSlotSelection(int idx)
        {
            _increaseRankButton.SetSelection(idx == RANK_UP_IDX && _activeSlotIdx != RANK_UP_IDX);
            for (int i = 0; i < _spellStringSlots.Length; i++)
            {
                _spellStringSlots[i].SetSelection(i == idx && _activeSlotIdx != i);
            }
        }

        private void ShowDefaultEffects()
        {
            _editorComponents.ShowDefaultEffects(_spellData);
            UpdateSlotSelection(EMPTY_IDX);
            _activeSlotIdx = EMPTY_IDX;
        }

        private void SelectSpellLine(SpellString spellString)
        {
            _selectedString = spellString;
            _editorComponents.ShowSpellLineEffect(_spellData, spellString);
        }

        private void HandleConfirmClick()
        {
            if (_spellData.StringSlotIsEmpty(_activeSlotIdx))
            {
                _spellData.SetActiveString(_activeSlotIdx, _selectedString);
                _editorComponents.ShowSlotEffects(_spellData.GetSpellStringAt(_activeSlotIdx));
                return;
            }

            if (_spellData.spellHasMaxRank) return;
            if (_inventory.resources.TrySpendResource(ResourceType.magicDust, _spellbook.increaseRankCost))
            {
                _spellData.IncreaseRank();
                ShowDefaultEffects();
            }
        }

        private void ClearSelectedSlot()
        {
            if (_inventory.resources.TrySpendResource(ResourceType.magicDust, _spellbook.clearSlotCost))
            {
                _spellData.ClearStringSlot(_activeSlotIdx);
                _editorComponents.ShowEmptySlotOptions();
            }
        }

        void IObserver<KnownSpellSlot>.AddToObserve(KnownSpellSlot target)
        {
            target.OnEditButtonClick += Open;
        }

        void IObserver<KnownSpellSlot>.RemoveFromObserve(KnownSpellSlot target)
        {
            target.OnEditButtonClick -= Open;
        }

        void IObserver<SpellStringPreview>.AddToObserve(SpellStringPreview target)
        {
            target.OnClick += SelectSpellLine;
        }

        void IObserver<SpellStringPreview>.RemoveFromObserve(SpellStringPreview target)
        {
            target.OnClick -= SelectSpellLine;
        }

    }
}