using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	public interface IUsable
	{
	    void Use();
		AudioClip useSound { get; }
	}
}