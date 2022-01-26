using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Items
{
    public interface IUsable
	{
	    void UseItem(AbilityController abilityController);
		AudioClip useSound { get; }
	}
}