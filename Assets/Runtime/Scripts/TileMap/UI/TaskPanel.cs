using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Map.UI
{
    public class TaskPanel : MonoBehaviour, IPointerClickHandler
    {
		[SerializeField] Sprite _activeBg;
		[SerializeField] Sprite _inactiveBg;
		[SerializeField] CustomEvent _event;
		[Header("UI Elements")]
        [SerializeField] Image _locationIcon;
        [SerializeField] Image _background;
        [SerializeField] TextMeshProUGUI _locationName;
        [SerializeField] TextMeshProUGUI _locationTask;

		public void SetLocationIcon(Sprite sprite)
		{
			_locationIcon.sprite = sprite;
		}

		public void SetLocationName(string text)
		{
			_locationName.text = text;
		}

		public void SetTask(TaskData task)
		{
			_locationTask.text = task.taskText;
			_locationIcon.sprite = task.icon;
			_locationName.text = task.displayName;
			_background.sprite = task.isDone ? _activeBg : _inactiveBg;
		}

        public void OnPointerClick(PointerEventData eventData)
        {
			_event?.Invoke();
        }
    }
}

