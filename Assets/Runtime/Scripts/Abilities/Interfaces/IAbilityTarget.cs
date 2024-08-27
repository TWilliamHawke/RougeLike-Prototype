using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
	public interface IAbilityTarget
	{
		T GetComponent<T>();
		Transform transform { get; }
	}
}