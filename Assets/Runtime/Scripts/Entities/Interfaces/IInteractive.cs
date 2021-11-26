using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

namespace Entities
{
	public interface IInteractive
	{
		void Interact(PlayerCore player);
		Transform transform { get; }
	}
}