using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Leveling;

namespace Entities.PlayerScripts
{
    public class PlayerDataManager : MonoBehaviour
    {
        [Header("Injectors")]
        [SerializeField] Injector _saveDataInjector;
		[SerializeField] Injector _experienceStorageInjector;

        public void StartUp()
        {
			var experienceStorage = new ExperienceStorage();
			_experienceStorageInjector.SetDependency(ref experienceStorage);
			_saveDataInjector.AddInjectionTarget(experienceStorage);
        }
    }
}