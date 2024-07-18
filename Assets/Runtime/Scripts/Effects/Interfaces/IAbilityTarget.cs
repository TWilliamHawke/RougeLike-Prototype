using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
	public interface IAbilityTarget
	{
		T GetComponent<T>();
		Transform transform { get; }
	}
}