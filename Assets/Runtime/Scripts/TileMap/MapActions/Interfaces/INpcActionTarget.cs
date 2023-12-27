using System.Collections;
using System.Collections.Generic;
using Entities.NPC;
using UnityEngine;

namespace Map
{
	public interface INpcActionTarget : IAttackActionTarget
	{
        INPCInventory inventory { get; }
	}
}


