using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
	public interface IInstantEffect
	{
	    void Apply(IEffectTarget target, int power);
	}
}