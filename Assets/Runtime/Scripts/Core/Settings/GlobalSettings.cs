using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Settings
{
	public class GlobalSettings : ScriptableObject
	{
		[Range(1, 6)]
	    [SerializeField] float _animationSpeed = 1;

		public float animationSpeed => _animationSpeed;
	}
}