using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.DragAndDrop
{
    //used as prefab so MonoBehaviour instead of interface
    public abstract class DragableUIElement<T> : DragableUIElement
    {
        public abstract void ApplyData(T data);
    }

    [RequireComponent(typeof(RectTransform))]
    public abstract class DragableUIElement : MonoBehaviour
    {
        RectTransform _transform;

        public void UpdatePosition(Vector2 position)
        {
            _transform.anchoredPosition += position;
        }

        public void SetDragStartPosition(Vector2 startPos)
        {
            _transform = GetComponent<RectTransform>();
            _transform.anchoredPosition = startPos;
        }

        public Vector2 GetRaycastPosition()
        {
            //left top angle
            var shift = new Vector2(-_transform.sizeDelta.x / 2, _transform.sizeDelta.y / 2);
            return Mouse.current.position.ReadValue() + shift;
        }
    }
}