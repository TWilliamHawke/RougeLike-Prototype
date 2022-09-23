using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Map.Objects.UI
{
    public class TopLocationInfoPanel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Injector _selfInjector;
		[SerializeField] Sprite _activeBg;
		[SerializeField] Sprite _inactiveBg;
		[Header("UI Elements")]
        [SerializeField] Image _locationIcon;
        [SerializeField] Image _background;
        [SerializeField] TextMeshProUGUI _locationName;
        [SerializeField] TextMeshProUGUI _locationTask;

		public bool _isActive = false;

		public event UnityAction OnClick;

        private void Awake()
        {
            _selfInjector.AddDependency(this);
        }

		public void SetLocationIcon(Sprite sprite)
		{
			_locationIcon.sprite = sprite;
		}

		public void SetLocationName(string text)
		{
			_locationName.text = text;
		}

		public void SetTask(string text)
		{
			_locationTask.text = text;
		}

        public void OnPointerClick(PointerEventData eventData)
        {
			if(!_isActive) return;
            OnClick?.Invoke();
        }

		public void SetActive()
		{
			_isActive = true;
			_background.sprite = _activeBg;
		}

		public void SetInactive()
		{
			_isActive = false;
			_background.sprite = _inactiveBg;
		}
    }
}

