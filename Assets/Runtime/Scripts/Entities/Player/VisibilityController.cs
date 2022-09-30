using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.PlayerScripts
{
	public class VisibilityController : MonoBehaviour
	{
	    [SerializeField] int _viewingRange = 5;
		[SerializeField] Transform _fowClearZone1;
		[SerializeField] Transform _fowClearZone2;

		public void ChangeViewingRange()
		{
			_fowClearZone1.localScale = Vector3.one * _viewingRange;
			_fowClearZone2.localScale = Vector3.one * _viewingRange;
		}
		
	}
}