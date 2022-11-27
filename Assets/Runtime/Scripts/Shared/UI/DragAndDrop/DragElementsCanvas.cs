using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.DragAndDrop
{
    [RequireComponent(typeof(Canvas))]
    public class DragElementsCanvas : MonoBehaviour
    {
        [SerializeField] Injector _dragCanvasInjector;

        private void Awake()
        {
			_dragCanvasInjector.SetDependency(GetComponent<Canvas>());
        }
    }
}