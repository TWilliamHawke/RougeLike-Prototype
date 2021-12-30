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

		public event UnityAction<object> OnBeginDrag;
		public event UnityAction OnEndDrag;

		public Canvas canvas
		{
			get => _canvas;
			set => _canvas = value;
		}

		public void BeginDrag(object data)
		{
			OnBeginDrag?.Invoke(data);
		}


		public void EndDrag()
		{
			OnEndDrag?.Invoke();
		}

		
    }
}
