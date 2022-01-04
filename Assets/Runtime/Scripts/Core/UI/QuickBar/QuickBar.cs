using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.DragAndDrop;
using Items;
using Effects;
using Entities.Player;

namespace Core.UI
{
	public class QuickBar : MonoBehaviour
	{
		[SerializeField] Inventory _inventory;
		[SerializeField] PlayerStats _playerStats;
        [Header("UI Elements")]
	    [SerializeField] QuickBarSlot[] _quickBarSlots;

        public void Init()
        {
			ItemContainer.SetInventory(_inventory);
			SpellContainer.SetManaComponent(_playerStats);
            SetUpSlotNumbers();
        }

        private void SetUpSlotNumbers()
        {
            for (int i = 0; i < _quickBarSlots.Length; i++)
            {
                try
                {
                    _quickBarSlots[i].SetSlotNumber(i);
                }
                catch (System.Exception)
                {

                    Debug.Log(i);
                }
            }
        }
    }
}