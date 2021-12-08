using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using Entities.UI;
using Items.UI;

namespace Core
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] TileInfoPanel _tileInfoPanel;
		[SerializeField] HealthbarController _healthbarCanvas;
		[SerializeField] InventoryScreen _inventoryScreen;

	    public void StartUp()
		{
			//subscribe on hovered tile event events
			_tileInfoPanel.Init();
			
			//subscribe on playerCreation events
			_healthbarCanvas.Init();
			//subscribe on inventory sections events
			_inventoryScreen.Init();
		}
	}
}