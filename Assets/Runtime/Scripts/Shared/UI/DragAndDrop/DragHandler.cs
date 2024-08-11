using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace UI.DragAndDrop
{

    public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IInjectionTarget
    {
        [InjectField] Canvas _dragCanvas;
        [SerializeField] Injector _dragCanvasInjector;

        [SerializeField] CustomEvent _onDragStart;
        [SerializeField] CustomEvent _onDragEnd;

        public event UnityAction OnDragStart;
        public event UnityAction OnDragEnd;

        IDragDataSource _dragDataSource;
        DragableUIElement _dragableElement;
        IDropTarget _dropTarget;

        public bool waitForAllDependencies => false;

        void Awake()
        {
            _dragCanvasInjector.AddInjectionTarget(this);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if (_dragDataSource is null)
            {
                _dragDataSource = GetComponent<IDragDataSource>();
            }
            if (!_dragDataSource.allowToDrag) return;
            _dragableElement = _dragDataSource.CreateElement();
            _dragableElement.SetParent(_dragCanvas);
            var mousePos = Mouse.current.position.ReadValue();
            var startPos = mousePos - _dragCanvas.renderingDisplaySize / 2;

            _dragableElement.SetDragStartPosition(startPos);
            OnDragStart?.Invoke();
            _onDragStart?.Invoke();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (!_dragDataSource.allowToDrag) return;

            _dragableElement.UpdatePosition(eventData.delta);
            var raycast = _dragableElement.GetRaycastPosition();
            _dragDataSource.TryFindDropTarget(out var nextTarget, raycast);
            if (nextTarget == _dropTarget) return;

            _dropTarget?.UnHighlight();
            nextTarget?.Highlight();
            _dropTarget = nextTarget;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (!_dragDataSource.allowToDrag) return;
            _dragDataSource.DropData();
            OnDragEnd?.Invoke();
            _onDragEnd?.Invoke();
            _dropTarget = null;
            _dragableElement = null;
        }
    }
}