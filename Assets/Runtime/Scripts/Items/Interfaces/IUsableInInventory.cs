using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Items
{
    public interface IUsableInInventory
	{
	    void UseItem(AbilityController abilityController);
		AudioClip useSound { get; }
		bool destroyAfterUse { get; }
	}
}