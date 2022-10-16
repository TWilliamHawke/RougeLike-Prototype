using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;

namespace Leveling
{
	public class ExperienceBarController: IInjectionTarget
	{
	    ExperienceBar _experienceBar;
		[InjectField] ExperienceStorage _experienceStorage;

        public ExperienceBarController(ExperienceBar experienceBar)
        {
            _experienceBar = experienceBar;
        }

        public bool waitForAllDependencies => false;

        public void FinalizeInjection()
        {
			_experienceStorage.OnGettingExp += UpdateBar;
			UpdateBar();
        }

        private void UpdateBar()
		{
			float totalExp = _experienceStorage.playerExp;
			int currentLevel = _experienceStorage.playerLevel;
			float expToNextLevel = _experienceStorage.GetExpToNextLevel();
			float totalExpToCurrentLevel = _experienceStorage.GetTotalExpToReachLevel(currentLevel);

			float expToNextLevelPct = (totalExp - totalExpToCurrentLevel) / expToNextLevel;

			expToNextLevelPct = Mathf.Clamp01(expToNextLevelPct);
			_experienceBar.SetExpPct(expToNextLevelPct);
		}
    }
}

