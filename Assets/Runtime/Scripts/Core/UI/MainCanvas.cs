using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;

namespace Core
{
	public class MainCanvas : MonoBehaviour
	{
	    [SerializeField] QuickBar _quickBar;
		[SerializeField] TileInfoPanel _tileInfoPanel;
		[SerializeField] StatPanel _statPanel;

		public void Init()
		{
			_quickBar.Init();
			_tileInfoPanel.Init();
			_statPanel.Init();
		}
	}
}