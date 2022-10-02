using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Items.UI;

namespace Map.Objects.UI
{
    public class ActionsScreen : MonoBehaviour, IUIScreen, IDependency
    {
		[SerializeField] Injector _actionsScreenInjector;
		[SerializeField] Injector _lootScreenInjector;
		[Header("UI Elements")]
		[SerializeField] TextMeshProUGUI _title;
		[SerializeField] Image _objectIcon1;
		[SerializeField] Image _objectIcon2;
		[SerializeField] Button _closeButton;
		[SerializeField] ActionButtonsPanel _actionButtonsPanel;

        public event UnityAction OnReadyForUse;

        public bool isReadyForUse => true;

        public void Init()
        {
            _closeButton.onClick.AddListener(Close);
			OnReadyForUse?.Invoke();
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

		public void SetActions(IMapActionsController actionLogics)
		{
			_actionButtonsPanel.SetActions(actionLogics);
		}

		void Close()
		{
			gameObject.SetActive(false);
		}
    }
}

