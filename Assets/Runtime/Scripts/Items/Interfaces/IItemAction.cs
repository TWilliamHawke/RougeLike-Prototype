using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	public interface IItemAction
	{
		string menuName { get; }
	    void DoIt();
	}
}