using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.DragAndDrop
{

    public abstract class DragHandler<T> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        [SerializeField] DragController _dragController;

        IDragDataSource<T> _dataSource;
        DragableUIElement<T> _dragableElement;

        private void Awake()
        {
            _dataSource = GetComponent<IDragDataSource<T>>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragableElement = Instantiate(_dataSource.dragableElementPrefab);
            _dragableElement.transform.SetParent(_dragController.canvas.transform);
            _dragableElement.SetData(_dataSource.dragData);
            _dragableElement.SetDefaultPosition();
            //_dragableElement.UpdatePosition(Mouse.current.position.ReadValue());
        }

        public void OnDrag(PointerEventData eventData)
        {
            _dragableElement.UpdatePosition(eventData.delta);

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _dragableElement.TryFindDropTarget();
            
            Destroy(_dragableElement.gameObject);
        }
    }
}