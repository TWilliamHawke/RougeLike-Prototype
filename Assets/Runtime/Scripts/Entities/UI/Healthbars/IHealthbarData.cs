using System.Collections;
using System.Collections.Generic;
using Entities.Behavior;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
	public interface IHealthbarData
	{
	    Vector3 bodyPosition { get; }
		int maxHealth { get; }
		int currentHealth { get; }
        BehaviorType behavior { get; }
		event UnityAction OnHealthChange;
    }
}