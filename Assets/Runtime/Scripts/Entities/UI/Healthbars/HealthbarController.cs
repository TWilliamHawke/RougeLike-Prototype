using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.UI
{
	public class HealthbarController : MonoBehaviour
	{
		[SerializeField] Healthbar _healthbarPrefab;

		public void Init()
		{
			Health.OnEntityEnabled += CreateNewHealthbar;
		}

		private void OnDestroy() {
			Health.OnEntityEnabled -= CreateNewHealthbar;
			
		}

		void CreateNewHealthbar(Health entityHealth)
		{
			var healthbar = Instantiate(_healthbarPrefab, transform);
			healthbar.SetEntity(entityHealth);
		}
	}
}