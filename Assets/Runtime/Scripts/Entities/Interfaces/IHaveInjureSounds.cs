using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
	public interface IHaveInjureSounds
	{
	    AudioClip[] deathSounds { get; }

	}
}