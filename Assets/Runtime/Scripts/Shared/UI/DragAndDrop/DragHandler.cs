using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace UI.DragAndDrop
{

    public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IInjectionTarget
    {
        [InjectField] Canvas _dragCanvas;
        [SerializeField] Injector _dragCanvasInjector;

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

        public void OnBeginDrag(PointerEventData eventData)
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
        }

        public void OnDrag(PointerEventData eventData)
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

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_dragDataSource.allowToDrag) return;
            _dragDataSource.DropData();
            OnDragEnd?.Invoke();
            _dropTarget = null;
            _dragableElement = null;
        }
    }

    public interface IDragController
    {
        bool TryFindDropTarget(out IDropTarget target, Vector2 raycastPos);
        DragableUIElement CreateElement();
        void DropData();
    }

    public class DragController<T> : IDragController
    {
        static ObjectPool<DragableUIElement<T>> _dragPool;

        IDragDataSource<T> _dataSource;
        DragableUIElement<T> _dragableElementPrefab;
        DragableUIElement<T> _createdElement;

        public DragableUIElement CreateElement()
        {
            _createdElement = _dragPool.Get();
            _createdElement.ApplyData(_dataSource.dragData);
            return _createdElement;
        }

        public DragController(IDragDataSource<T> dataSource, DragableUIElement<T> dragableElement)
        {
            _dataSource = dataSource;
            _dragableElementPrefab = dragableElement;

            if (_dragPool is not null) return;

            _dragPool = new(
                createFunc: () => GameObject.Instantiate(_dragableElementPrefab),
                actionOnGet: elem => elem.gameObject.SetActive(true),
                actionOnRelease: elem => elem.gameObject.SetActive(false),
                defaultCapacity: 1
            );
        }

        public void DropData()
        {
            var raycastPos = _createdElement.GetRaycastPosition();
            TryFindGenericTarget(out var target, raycastPos);
            target?.UnHighlight();
            target?.DropData(_dataSource.dragData);
            _dragPool.Release(_createdElement);
        }

        public bool TryFindDropTarget(out IDropTarget target, Vector2 raycastPos)
        {
            bool isSuccess = TryFindGenericTarget(out var genericTarget, raycastPos);
            target = genericTarget;
            return isSuccess;
        }

        public bool TryFindGenericTarget(out IDropTarget<T> target, Vector2 raycastPos)
        {
            target = default;
            var hits = Raycasts.UI(raycastPos);

            foreach (var hit in hits)
            {
                if (hit.gameObject.TryGetComponent<IDropTarget<T>>(out var possibleTarget)
                    && TargetIsValid(possibleTarget, hit, raycastPos))
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