using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map
{
	public interface IMapActionsController: IMapActionList, IActionLogicConsumer
	{
		event UnityAction OnActionStateChange;
	}

	public interface IMapActionList
	{
	    IMapActionLogic this[int idx] { get; }
		int count { get; }
	}

	public interface IActionLogicConsumer
	{
		void AddLogic(IActionLogicCreator actionLogicCreator);
		void AddLogic(IMapActionLogic actionLogic);
	}
}

