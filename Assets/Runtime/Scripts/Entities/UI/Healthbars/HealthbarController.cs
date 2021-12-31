using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.UI
{
    public class HealthbarController : MonoBehaviour
    {
        [SerializeField] Healthbar _healthbarPrefab;

        Healthbar _playerHealthbar;

        public void Init()
        {
            Health.OnHealthInit += CreateNewHealthbar;
        }

        void OnDestroy()
        {
            Health.OnHealthInit -= CreateNewHealthbar;
        }

        void CreateNewHealthbar(IHealthbarData entityHealth)
        {
            var healthbar = Instantiate(_healthbarPrefab, transform);
            healthbar.SetHealth(entityHealth);
            healthbar.SetColor(entityHealth.healthbarColor);
        }

        // void CreatePlayerHealthbar()
        // {
        //     _playerHealthbar = Instantiate(_healthbarPrefab, transform);
        //     _playerHealthbar.SetHealth(_gameObjects.player.body);
        //     _playerHealthbar.SetColor(_playerHealthbarColor);
        // }
    }
}