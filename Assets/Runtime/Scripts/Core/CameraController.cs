using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	[RequireComponent(typeof(Camera))]
	public class CameraController : MonoBehaviour
	{
	    public void CenterAt(Vector3 pos)
		{
			transform.position = transform.position.ChangeXYFrom(pos);
		}

	    public void CenterAt(Vector2 pos)
		{
			transform.position = transform.position.ChangeXYFrom(pos);
		}
	}
}