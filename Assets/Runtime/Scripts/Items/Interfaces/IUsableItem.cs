using System.Collections;
using System.Collections.Generic;
using Abilities;
using Effects;
using UnityEngine;

namespace Items
{
	public interface IUsableItem
	{
		AudioClip useSound { get; }
		bool destroyAfterUse { get; }
		void Use();
	}
}