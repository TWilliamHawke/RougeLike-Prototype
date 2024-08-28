using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using Leveling;

namespace Core
{
	public class MainCanvas : MonoBehaviour
	{
		[SerializeField] TileInfoPanel _tileInfoPanel;
		[SerializeField] ActiveEffectsPanel _activeEffectsPanel;
		[SerializeField] ExperienceBar _experienceBar;
		[SerializeField] InfoButton _infoButton;
		[Header("Injectors")]
		[SerializeField] Injector _infoButtonInjector;
		[SerializeField] Injector _experienceStorageInjector;

		ExperienceBarController _expBarController;

		public void Init()
		{
			_tileInfoPanel.Init();
			_expBarController = new ExperienceBarController(_experienceBar);
			_experienceStorageInjector.AddInjectionTarget(_expBarController);
			_infoButtonInjector.SetDependency(_infoButton);
		}
	}
}