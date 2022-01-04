using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
	public interface IManaComponent
	{
	    int maxMana { get; }
		int curentMana { get; }
		bool TrySpendMana(int count);
	}
}