using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Entities.Player
{
	public class PlayerDataManager : MonoBehaviour
	{
		[SerializeField] Inventory _inventory;
		[SerializeField] StoredResources _resources;

	    public void StartUp()
		{
			_inventory.Init();
			_resources.Init();
		}
	}
}