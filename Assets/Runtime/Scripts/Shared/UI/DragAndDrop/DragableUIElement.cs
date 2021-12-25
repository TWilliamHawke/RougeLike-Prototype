using System;
using System.Collections;
using System.Collections.Generic;
using Magic.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.DragAndDrop
{
    //used as prefab so MonoBehaviour instead of interface
    [RequireComponent(typeof(RectTransform))]
    public abstract class DragableUIElement<T> : MonoBehaviour
    {
        [SerializeField] DragController _dragController;
        T _data;

        RectTransform _transform;

        public void SetData(T data)
        {
            _data = data;
            ApplyData(data);
        }

        protected abstract void ApplyData(T data);

        public void UpdatePosition(Vector2 position)
        {
            _transform.anchoredPosition += position;
        }

        public void SetDefaultPosition()
        {
            _transform = GetComponent<RectTransform>();
            var startPos = Mouse.current.position.ReadValue() - _dragController.canvas.renderingDisplaySize/2;
            _transform.anchoredPosition = startPos;
        }

        public void TryFindDropTarget()
        {
            //left top angle
            var shift = new Vector2(-_transform.sizeDelta.x/2, _transform.sizeDelta.y/2);
            var raycastPosition = Mouse.current.position.ReadValue() + shift;
            var hits = Raycasts.UI(raycastPosition);

            foreach (var hit in hits)
            {
                if(hit.gameObject.TryGetComponent<IDropTarget<T>>(out var target))
                {
                    if(target.DataIsMeet(_data))
                    {
                        target.SetDragableData(_data);
                    }
                    return;
                }
            }

        }
    }
}