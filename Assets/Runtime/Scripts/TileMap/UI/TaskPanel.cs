using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Map.UI
{
    public class TaskPanel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Color _activeColor = Color.olive;
        [SerializeField] Color _inactiveColor = Color.magenta;
        [SerializeField] CustomEvent _event;
        [Header("UI Elements")]
        [SerializeField] Image _locationIcon;
        [SerializeField] Image _background;
        [SerializeField] TextMeshProUGUI _locationName;
        [SerializeField] TextMeshProUGUI _locationTask;

        public event UnityAction OnPanelClick;

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
            //onTriggerExit2d invokes then scene was destroyed
            if (_locationIcon.IsDestroyed()) return;

            _locationTask.text = task.taskText;
            _locationIcon.sprite = task.icon;
            _locationName.text = task.displayName;
            _background.color = task.isDone ? _activeColor : _inactiveColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _event?.Invoke();
            OnPanelClick?.Invoke();
        }
    }
}

