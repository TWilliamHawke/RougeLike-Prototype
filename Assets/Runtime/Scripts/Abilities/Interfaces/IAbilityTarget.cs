using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public interface IAbilityTarget
    {
        T GetComponent<T>();
        Vector3 position { get; }
        void MoveTo(Vector3 position);
	}
}