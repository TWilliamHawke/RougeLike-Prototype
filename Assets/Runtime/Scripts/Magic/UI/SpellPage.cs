using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Entities.PlayerScripts;

namespace Magic.UI
{
    public class SpellPage : MonoBehaviour, IObserver<KnownSpellSlot>
    {
        const int EMPTY_IDX = -2;

        [SerializeField] UIScreen _editorScreen;
        [Header("UI Elements")]
        [SerializeField] Image _spellIcon;
        [SerializeField] TextMeshProUGUI _spellName;
        [SerializeField] TextMeshProUGUI _spellRank;
        [SerializeField] TextMeshProUGUI _spellCost;
        [SerializeField] TextMeshProUGUI _spellDescription;

        [SerializeField] ActiveStringSlot[] _spellStringSlots;
        [SerializeField] ActiveStringSlot _increaseRankButton;
        [SerializeField] SpellList _spellList;

        int _activeStringSlot = EMPTY_IDX;

        KnownSpellData _spellData;

        public void Start()
        {
            _spellList.AddObserver(this);
            _increaseRankButton.OnClick += OpenIncreaseRankPanel;

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
            spellData.OnChangeData += UpdateUIData;
            UpdateUIData();
            _editorScreen.Open();
        }

        void UpdateUIData()
        {
            _spellIcon.sprite = _spellData.icon;
            _spellName.text = _spellData.displayName;
            _spellRank.text = "Rank " + _spellData.rank;
            _spellCost.text = _spellData.manaCost.ToString();
            _spellDescription.text = _spellData.ConstructDescription();
            UpdateActiveSlots();
        }

        void UpdateActiveSlots()
        {
            _increaseRankButton.gameObject.SetActive(!_spellData.spellHasMaxRank);
            for (int i = 0; i < _spellStringSlots.Length; i++)
            {
                var slot = _spellStringSlots[i];
                if (i >= _spellData.rank || i >= _spellData.activeStrings.Length)
                {
                    slot.Disable();
                    continue;
                }

                slot.Enable();
                slot.SetIcon(_spellData.activeStrings[i]);
            }
        }

        void OpenIncreaseRankPanel(int _)
        {
            SelectSlot(-1);
            Debug.Log("OpenIncreaseRankPanel");
            _spellData.IncreaseRank();
            UpdateUIData();
        }

        void SelectSlot(int idx)
        {
            _increaseRankButton.SetSelection(idx == -1 && _activeStringSlot != -1);
            for (int i = 0; i < _spellStringSlots.Length; i++)
            {
                _spellStringSlots[i].SetSelection(i == idx && _activeStringSlot != i);
            }
            _activeStringSlot = _activeStringSlot == idx ? EMPTY_IDX : idx;
        }

        void IObserver<KnownSpellSlot>.AddToObserve(KnownSpellSlot target)
        {
            target.OnEditButtonClick += Open;
        }

        void IObserver<KnownSpellSlot>.RemoveFromObserve(KnownSpellSlot target)
        {
            target.OnEditButtonClick -= Open;
        }
    }
}