using System.Collections;
using System.Collections.Generic;
using Entities.Behavior;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
	public interface IHaveHealthData : IFactionMember
	{
	    AudioClip[] deathSounds { get; }
		int maxHealth { get; }
	}
}