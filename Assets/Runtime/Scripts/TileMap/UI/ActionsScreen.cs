using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Map.UI
{
	[RequireComponent(typeof(UIScreen))]
    public class ActionsScreen : MonoBehaviour
    {
		[SerializeField] Injector _actionsScreenInjector;
		[SerializeField] Injector _lootScreenInjector;
		[Header("UI Elements")]
		[SerializeField] TextMeshProUGUI _title;
		[SerializeField] Image _objectIcon1;
		[SerializeField] Image _objectIcon2;
		[SerializeField] ActionButtonsPanel _actionButtonsPanel;

		UIScreen _UIScreen;

        public void Init()
        {
			_actionsScreenInjector.SetDependency(this);
			_UIScreen = GetComponent<UIScreen>();
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

		public void Open()
		{
			_UIScreen.Open();
		}
    }
}

