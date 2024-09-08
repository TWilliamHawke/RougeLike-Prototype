using System.Collections;
using System.Collections.Generic;
using Entities.NPC;
using Items;
using UnityEngine;

namespace Map
{
	public interface INpcActionTarget : IAttackActionTarget
	{
        IContainersList inventory { get; }
	}
}


