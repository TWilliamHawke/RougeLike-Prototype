using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
	public interface IObstacleEntity : IEntityWithComponents
	{
		Transform transform { get; }
	}
}


