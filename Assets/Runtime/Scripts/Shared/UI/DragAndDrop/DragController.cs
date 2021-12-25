using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UI.DragAndDrop
{

    [CreateAssetMenu(menuName = "UI Controllers/DragController", fileName = "DragController")]
    public class DragController : ScriptableObject
    {
		static Canvas _canvas;

		public event UnityAction OnBeginDrag;
		public event UnityAction OnEndDrag;

		public Canvas canvas
		{
			get => _canvas;
			set => _canvas = value;
		}

		public void BeginDrag()
		{
			OnBeginDrag?.Invoke();
		}


		public void EndDrag()
		{
			OnEndDrag?.Invoke();
		}

		
    }
}
