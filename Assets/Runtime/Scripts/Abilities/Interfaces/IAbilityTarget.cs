using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Abilities
{
    public interface IAbilityTarget
    {
        T GetEntityComponent<T>() where T : IEntityComponent;
	}
}