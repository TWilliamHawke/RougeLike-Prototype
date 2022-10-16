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
		[SerializeField] InfoButton _infoButton;
		[Header("Injectors")]
		[SerializeField] Injector _infoButtonInjector;
		[SerializeField] Injector _experienceStorageInjector;

		ExperienceBarController _expBarController;

		public void Init()
		{
			_quickBar.Init();
			_tileInfoPanel.Init();
			_statPanel.Init();
			_activeEffectsPanel.Init();
			_expBarController = new ExperienceBarController(_experienceBar);
			_experienceStorageInjector.AddInjectionTarget(_expBarController);
			_infoButtonInjector.AddDependency(_infoButton);
		}
	}
}