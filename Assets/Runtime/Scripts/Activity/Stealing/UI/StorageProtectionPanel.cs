using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Items.UI
{
    public class StorageProtectionPanel : UIElement
    {
        [SerializeField] string _baseButtonText;
        [SerializeField] string _baseLevelText;
        [Header("UI Elements")]
        [SerializeField] Button _mainButton;
        [SerializeField] TextMeshProUGUI _buttonText;
        [SerializeField] TextMeshProUGUI _levelText;
        [SerializeField] TextMeshProUGUI _warningText;

        public event UnityAction OnDisable;

        void Start()
        {
            _mainButton.onClick.AddListener(DisableProtection);
        }

        public void ResetProtection()
        {
            _mainButton.interactable = true;
        }

        public void SetSkillProtection(int playerSkill, int requiredSkill)
        {
            bool canDisable = playerSkill >= requiredSkill;
            _mainButton.interactable = _mainButton.interactable && canDisable;
            _warningText.gameObject.SetActive(!canDisable);
            _levelText.text = _baseLevelText + requiredSkill;
        }

        public void SetCostProtection(int aviablePoins, int requiredPoints)
        {
            bool canDisable = aviablePoins >= requiredPoints;
            _mainButton.interactable = _mainButton.interactable && canDisable;
            _buttonText.text = _baseButtonText += requiredPoints;
        }

        private void DisableProtection()
        {
            OnDisable?.Invoke();
        }

    }
}
