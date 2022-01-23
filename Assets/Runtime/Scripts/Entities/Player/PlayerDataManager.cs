using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Entities.Player
{
	public class PlayerDataManager : MonoBehaviour
	{
		[SerializeField] Inventory _inventory;

	    public void StartUp()
		{
			_inventory.Init();
		}
	}
}