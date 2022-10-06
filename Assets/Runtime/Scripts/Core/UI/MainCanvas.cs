using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using Leveling;

namespace Core
{
	public class MainCanvas : MonoBehaviour
	{
	    [SerializeField] QuickBar _quickBar;
		[SerializeField] TileInfoPanel _tileInfoPanel;
		[SerializeField] StatPanel _statPanel;
		[SerializeField] ActiveEffectsPanel _activeEffectsPanel;
		[SerializeField] ExperienceBar _experienceBar;
		[SerializeField] ExperienceStorage _experienceStorage;
		[SerializeField] InfoButton _infoButton;
		[SerializeField] Injector _infoButtonInjector;

		ExperienceBarController _expBarController;

		public void Init()
		{
			_quickBar.Init();
			_tileInfoPanel.Init();
			_statPanel.Init();
			_activeEffectsPanel.Init();
			_expBarController = new ExperienceBarController(_experienceBar, _experienceStorage);
			_infoButtonInjector.AddDependency(_infoButton);
		}
	}
}