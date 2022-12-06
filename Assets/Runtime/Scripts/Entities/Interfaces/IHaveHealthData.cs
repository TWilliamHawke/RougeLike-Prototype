using System.Collections;
using System.Collections.Generic;
using Entities.Behavior;
using UnityEngine;

namespace Entities
{
	public interface IHaveHealthData
	{
	    AudioClip[] deathSounds { get; }
		int maxHealth { get; }
		BehaviorType antiPlayerBehavior { get; }
	}
}