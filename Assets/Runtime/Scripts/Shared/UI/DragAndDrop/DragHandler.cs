using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

namespace UI.DragAndDrop
{

    public class DragHandler<T>
    {
        static ObjectPool<DragableUIElement<T>> _dragPool;

        IDragDataSource<T> _dataSource;
        DragableUIElement<T> _dragableElement;

        public DragHandler(IDragDataSource<T> dataSource)
        {
            _dataSource = dataSource;
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
            _dragableElement.SetParent(_dataSource.dragCanvas);
            _dragableElement.SetData(_dataSource);
            _dragableElement.SetDefaultPosition();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _dragableElement.UpdatePosition(eventData.delta);

        }

        public void OnEndDrag()
        {
            _dragableElement.TryFindDropTarget();
            _dragPool.Release(_dragableElement);
        }
    }
}