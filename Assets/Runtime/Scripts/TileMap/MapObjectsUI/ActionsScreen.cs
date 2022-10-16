using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Map.Objects.UI
{
    public class ActionsScreen : MonoBehaviour, IUIScreen
    {
		[SerializeField] Injector _actionsScreenInjector;
		[SerializeField] Injector _lootScreenInjector;
		[Header("UI Elements")]
		[SerializeField] TextMeshProUGUI _title;
		[SerializeField] Image _objectIcon1;
		[SerializeField] Image _objectIcon2;
		[SerializeField] Button _closeButton;
		[SerializeField] ActionButtonsPanel _actionButtonsPanel;

        public void Init()
        {
            _closeButton.onClick.AddListener(Close);
			_actionsScreenInjector.AddDependency(this);
        }

		public void Open()
		{
			gameObject.SetActive(true);
		}

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

		void Close()
		{
			gameObject.SetActive(false);
		}
    }
}

