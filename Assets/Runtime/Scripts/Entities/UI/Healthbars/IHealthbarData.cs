using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
	public interface IHealthbarData
	{
	    Transform transform { get; }
		int maxHealth { get; }
		int currentHealth { get; }
		event UnityAction OnHealthChange;
		Color healthbarColor { get; }
	}
}