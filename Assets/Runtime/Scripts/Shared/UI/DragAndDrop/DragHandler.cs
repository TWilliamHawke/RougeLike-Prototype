using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace UI.DragAndDrop
{

    public class DragHandler<T>
    {
        static ObjectPool<DragableUIElement<T>> _dragPool;

        IDragDataSource<T> _dataSource;
        DragableUIElement<T> _dragableElement;
        IDropTarget<T> _prevTarget;
        Canvas _dragCanvas;

        public DragHandler(IDragDataSource<T> dataSource, Canvas dragCanvas)
        {
            _dataSource = dataSource;
            _dragCanvas = dragCanvas;
            if (_dragPool is not null) return;

            _dragPool = new(
                createFunc: () => GameObject.Instantiate(_dataSource.dragableElementPrefab),
                actionOnGet: elem => elem.gameObject.SetActive(true),
                actionOnRelease: elem => elem.gameObject.SetActive(false),
                defaultCapacity: 1
            );
        }

        public void OnBeginDrag()
        {
            _dragableElement = _dragPool.Get();
            _dragableElement.SetParent(_dragCanvas);
            _dragableElement.ApplyData(_dataSource.dragData);
            var startPos = Mouse.current.position.ReadValue() - _dragCanvas.renderingDisplaySize / 2;

            _dragableElement.SetDragStartPosition(startPos);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _dragableElement.UpdatePosition(eventData.delta);
            TryFindDropTarget(out var target);
            if (target == _prevTarget) return;

            _prevTarget?.UnHighlight();
            target?.Highlight();
            _prevTarget = target;
        }

        public void OnEndDrag()
        {
            TryFindDropTarget(out var target);
            target?.UnHighlight();
            target?.DropData(_dataSource.dragData);
            _prevTarget = target = null;
            _dragPool.Release(_dragableElement);
        }

        bool TryFindDropTarget(out IDropTarget<T> target)
        {
            target = default;
            var raycastPosition = _dragableElement.GetRaycastPosition();
            var hits = Raycasts.UI(raycastPosition);

            foreach (var hit in hits)
            {
                if (hit.gameObject.TryGetComponent<IDropTarget<T>>(out var possibleTarget)
                    && TargetIsValid(possibleTarget, hit, raycastPosition))
                {
                    target = possibleTarget;
                    return true;
                }
            }
            return false;
        }

        bool TargetIsValid(IDropTarget<T> target, RaycastResult hit, Vector3 raycastPosition)
        {
            if (!target.DataIsMeet(_dataSource.dragData)) return false;
            if (!target.checkImageAlpha) return true;
            if (!hit.gameObject.TryGetComponent<Image>(out var image)) return true;
            return Raycasts.IsRaycastLocationValid(raycastPosition, image);
        }

    }
}