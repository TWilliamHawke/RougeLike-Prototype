using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map
{
	public interface ITaskController
	{
		TaskData currentTask { get; }
	}
}


