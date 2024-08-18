using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace UI.DragAndDrop
{
    public class DragController<T> : IDragController
    {
        static ObjectPool<DragableUIElement<T>> _dragPool;

        //Data can be changed before drag start, T doesn't fit
        IDragDataSource<T> _dataSource;
        DragableUIElement<T> _prefab;
        DragableUIElement<T> _createdElement;

        public DragController(IDragDataSource<T> dataSource, DragableUIElement<T> prefab)
        {
            _dataSource = dataSource;
            _prefab = prefab;

            if (_dragPool is not null) return;

            _dragPool = new(
                createFunc: () => GameObject.Instantiate(_prefab),
                actionOnGet: elem => elem.gameObject.SetActive(true),
                actionOnRelease: elem => elem.gameObject.SetActive(false),
                defaultCapacity: 1
            );
        }

        public DragableUIElement CreateElement()
        {
            _createdElement = _dragPool.Get();
            _createdElement.ApplyData(_dataSource.dragData);
            return _createdElement;
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

        private bool TryFindGenericTarget(out IDropTarget<T> target, Vector2 raycastPos)
        {
            target = default;
            var hits = Raycasts.UI(raycastPos);

            foreach (var hit in hits)
            {
                if (hit.gameObject.TryGetComponent<IDropTarget>(out var possibleTarget)
                    && TargetIsValid(possibleTarget, hit, raycastPos))
                {
                    target = possibleTarget as IDropTarget<T>;
                    return true;
                }
            }
            return false;
        }

        private bool TargetIsValid(IDropTarget target, RaycastResult hit, Vector3 raycastPosition)
        {
            if (target is not IDropTarget<T> castedTarget) return false;
            if (!castedTarget.DataIsMeet(_dataSource.dragData)) return false;
            if (!target.checkImageAlpha) return true;
            if (!hit.gameObject.TryGetComponent<Image>(out var image)) return true;
            return Raycasts.IsRaycastLocationValid(raycastPosition, image);
        }

    }
}