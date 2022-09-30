using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;

namespace Leveling
{
	public class ExperienceBarController
	{
	    ExperienceBar _experienceBar;
		ExperienceStorage _experienceStorage;

        public ExperienceBarController(ExperienceBar experienceBar, ExperienceStorage experienceStorage)
        {
            _experienceBar = experienceBar;
            _experienceStorage = experienceStorage;
			_experienceStorage.OnGettingExp += UpdateBar;
			UpdateBar();
        }

		private void UpdateBar()
		{
			float totalExp = _experienceStorage.playerExp;
			int currentLevel = _experienceStorage.playerLevel;
			float expToNextLevel = _experienceStorage.GetExpToNextLevel();
			float expToCurrentLevel = _experienceStorage.GetTotalExpToReachLevel(currentLevel);

			float expToNextLevelPct = (totalExp - expToCurrentLevel) / expToNextLevel;
			expToNextLevelPct = Mathf.Clamp01(expToNextLevelPct);
			_experienceBar.SetExpPct(expToNextLevelPct);
		}
    }
}

