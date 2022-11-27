using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.DragAndDrop
{
    //used as prefab so MonoBehaviour instead of interface
    [RequireComponent(typeof(RectTransform))]
    public abstract class DragableUIElement<T> : MonoBehaviour
    {
        T _data;
        Canvas _dragCanvas;
        RectTransform _transform;

        public void SetData(IDragDataSource<T> dataSource)
        {
            _data = dataSource.dragData;
            _dragCanvas = dataSource.dragCanvas;
            ApplyData(_data);
        }

        protected abstract void ApplyData(T data);

        public void UpdatePosition(Vector2 position)
        {
            _transform.anchoredPosition += position;
        }

        public void SetDefaultPosition()
        {
            _transform = GetComponent<RectTransform>();
            var startPos = Mouse.current.position.ReadValue() - _dragCanvas.renderingDisplaySize / 2;
            _transform.anchoredPosition = startPos;
        }

        public void TryFindDropTarget()
        {
            //left top angle
            var shift = new Vector2(-_transform.sizeDelta.x / 2, _transform.sizeDelta.y / 2);
            var raycastPosition = Mouse.current.position.ReadValue() + shift;
            var hits = Raycasts.UI(raycastPosition);

            foreach (var hit in hits)
            {
                if (hit.gameObject.TryGetComponent<IDropTarget<T>>(out var target))
                {
                    if (target.DataIsMeet(_data))
                    {
                        target.DropData(_data);
                    }
                    break;
                }
            }

        }
    }
}