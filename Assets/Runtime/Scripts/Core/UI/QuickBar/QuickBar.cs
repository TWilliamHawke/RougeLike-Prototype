using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.DragAndDrop;
using Items;
using Effects;

namespace Core.UI
{
	public class QuickBar : MonoBehaviour
	{
	    [SerializeField] QuickBarSlot[] _quickBarSlots;

        public void Init()
		{
			for (int i = 0; i < _quickBarSlots.Length; i++)
			{
				_quickBarSlots[i].SetSlotNumber(i);
			}
		}
	}
}