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

		public void SetTask(MapObjectTask task)
		{
			_locationTask.text = task.taskText;
			if(task.objectIsLocked)
			{
				SetInactive();
			}
			else
			{
				SetActive();
			}
		}

        public void OnPointerClick(PointerEventData eventData)
        {
			if(!_isActive) return;
            OnClick?.Invoke();
        }

		void SetActive()
		{
			_isActive = true;
			_background.sprite = _activeBg;
		}

		void SetInactive()
		{
			_isActive = false;
			_background.sprite = _inactiveBg;
		}
    }
}

