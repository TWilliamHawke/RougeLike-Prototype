using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.DragAndDrop
{
	[RequireComponent(typeof(Canvas))]
	public class DragElementsCanvas : MonoBehaviour
	{
		[SerializeField] DragController _dragController;

	    public void Init()
		{
			_dragController.canvas = GetComponent<Canvas>();
		}
	}
}