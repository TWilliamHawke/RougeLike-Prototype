using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Input
{
	public interface IMouseClickState
	{
	    bool Condition();
		void ProcessClick();
	}
}