using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

namespace Effects
{
	[CreateAssetMenu(fileName = "RestoreHealth", menuName = "Effects/RestoreHealth")]
    public class RestoreHealth : Effect, IInstantEffect
    {
        public void Apply(IEffectTarget target, int power)
        {
            target.GetComponent<IHealthComponent>().RestoreHealth(power);
        }
    }
}