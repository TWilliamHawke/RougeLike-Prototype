using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Entities.PlayerScripts;

namespace Magic.UI
{
    public class SpellPage : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] Image _spellIcon;
        [SerializeField] TextMeshProUGUI _spellName;
        [SerializeField] TextMeshProUGUI _spellRank;
        [SerializeField] TextMeshProUGUI _spellCost;
        [SerializeField] TextMeshProUGUI _spellDescription;
        [SerializeField] IncreaseRankButton _rankUpButton;
        [SerializeField] ActiveStringSlot[] _spellStringSlots;

        KnownSpellData _spellData;

        public void Init()
        {
            KnownSpellData.OnChangeData += UpdateData;
            _rankUpButton.Init();

            for (int i = 0; i < _spellStringSlots.Length; i++)
            {
                _spellStringSlots[i].Init(i);
                
            }
        }

        void OnDestroy()
        {
            KnownSpellData.OnChangeData -= UpdateData;
        }


        public void Open(KnownSpellData spellData)
        {
            _spellData = spellData;
            SetUIData();
            gameObject.SetActive(true);
            _rankUpButton.UpdateState(spellData);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        void SetUIData()
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
            for (int i = 0; i < _spellStringSlots.Length; i++)
            {
                var slot = _spellStringSlots[i];
                if (i >= _spellData.rank || i >= _spellData.activeStrings.Length)
                {
                    slot.gameObject.SetActive(false);
                    continue;
                }

                slot.gameObject.SetActive(true);
                slot.SetData(_spellData);

            }
        }

        void UpdateData(KnownSpellData data)
        {
            if (data != _spellData) return;

            SetUIData();
        }

    }
}