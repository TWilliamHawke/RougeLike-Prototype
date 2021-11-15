using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;

namespace Core
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] TileInfoPanel _tileInfoPanel;

	    public void StartUp()
		{
			_tileInfoPanel.StartUp();
		}
	}
}