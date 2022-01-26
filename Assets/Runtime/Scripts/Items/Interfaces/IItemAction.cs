using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	public interface IItemAction
	{
		string actionTitle { get; }
	    void DoAction(ItemSlotData itemSlotData);
		bool SlotIsValid(IItemSlot itemSlot);
	}
}