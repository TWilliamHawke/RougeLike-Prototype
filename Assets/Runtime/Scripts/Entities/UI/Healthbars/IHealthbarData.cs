using System.Collections;
using System.Collections.Generic;
using Entities.Behavior;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
	public interface IHealthbarData : IEntityComponent
	{
	    Vector3 bodyPosition { get; }
    }
}