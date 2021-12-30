using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.DragAndDrop
{

    public class DragHandler<T>
    {

        DragController _dragController;

        IDragDataSource<T> _dataSource;

        public DragHandler(IDragDataSource<T> dataSource)
        {
            _dataSource = dataSource;
            _dragController = _dataSource.dragController;
        }

        DragableUIElement<T> _dragableElement;


        public void OnBeginDrag()
        {
            _dragableElement = Object.Instantiate(_dataSource.dragableElementPrefab);
            _dragableElement.transform.SetParent(_dragController.canvas.transform);
            _dragableElement.SetData(_dataSource.dragData);
            _dragableElement.SetDefaultPosition();
            _dragController.BeginDrag(_dataSource.dragData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _dragableElement.UpdatePosition(eventData.delta);

        }

        public void OnEndDrag()
        {
            _dragableElement.TryFindDropTarget();
            _dragableElement.DestroySelf();
            _dragController.EndDrag();
        }
    }
}