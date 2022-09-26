using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map
{
	public interface IMapActionsController: IEnumerable<IMapActionLogic>
	{
	    IMapActionLogic this[int idx] { get; }
		int count { get; }
		event UnityAction OnActionStateChange;
	}
}

