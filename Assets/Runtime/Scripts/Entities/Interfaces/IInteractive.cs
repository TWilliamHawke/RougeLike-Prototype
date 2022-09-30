using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using UnityEngine;

namespace Entities
{
	public interface IInteractive
	{
		void Interact(Player player);
		Transform transform { get; }
	}
}