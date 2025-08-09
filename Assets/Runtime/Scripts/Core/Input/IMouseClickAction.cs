using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Input
{
	public interface IMouseClickAction
	{
	    bool Condition();
		void ProcessClick();
	}
}