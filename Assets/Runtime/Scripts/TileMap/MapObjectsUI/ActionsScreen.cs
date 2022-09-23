using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Items.UI;

namespace Map.Objects.UI
{
    public class ActionsScreen : MonoBehaviour, IUIScreen, IInjectionTarget, IDependency,
		IActionDependenciesProvider
    {
		[SerializeField] Injector _actionsScreenInjector;
		[SerializeField] Injector _lootScreenInjector;
		[Header("UI Elements")]
		[SerializeField] TextMeshProUGUI _title;
		[SerializeField] Image _objectIcon1;
		[SerializeField] Image _objectIcon2;
		[SerializeField] Button _closeButton;
		[SerializeField] ActionButtonsPanel _actionButtonsPanel;

		[InjectField] LootPanel _lootPanel;

        public event UnityAction OnReadyForUse;

        public bool waitForAllDependencies => true;

        public bool isReadyForUse => _lootPanel != null;
        public LootPanel lootPanel => _lootPanel;

        public void Init()
        {
            _closeButton.onClick.AddListener(Close);
			_actionsScreenInjector.AddDependency(this);
			_lootScreenInjector.AddInjectionTarget(this);
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

		public void SetActions(List<IMapActionLogic> actionLogics)
		{
			foreach(var action in actionLogics)
			{
				action.AddActionDependencies(this);
			}
			_actionButtonsPanel.SetActions(actionLogics);
		}

		void Close()
		{
			gameObject.SetActive(false);
		}

        public void FinalizeInjection()
        {
			Debug.Log("LootPanel");
            OnReadyForUse?.Invoke();
        }
    }
}

