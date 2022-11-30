using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Items
{
	public interface IItemAction: IContextAction
	{
		IItemSlot itemSlot { get; set; }
		bool SlotIsValid(IItemSlot itemSlot);
	}
}