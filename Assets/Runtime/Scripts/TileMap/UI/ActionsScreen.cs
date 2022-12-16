using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Map.UI
{
    public class ActionsScreen : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] TextMeshProUGUI _title;
        [SerializeField] Image _objectIcon1;
        [SerializeField] Image _objectIcon2;
        [SerializeField] ActionButtonsPanel _actionButtonsPanel;

        public void SetTitle(string text)
        {
            _title.text = text;
        }

        public void SetIcon(Sprite icon)
        {
            _objectIcon1.sprite = icon;
            _objectIcon2.sprite = icon;
        }

        public void SetActions(IMapActionList actionLogics)
        {
            _actionButtonsPanel.SetActions(actionLogics);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}

