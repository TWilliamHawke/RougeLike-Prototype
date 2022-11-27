using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.UI
{
    public class HealthbarCanvas : MonoBehaviour
    {
        [SerializeField] Healthbar _healthbarPrefab;
        [SerializeField] Injector _healthbarCanvasInjector;

        private void Awake()
        {
            _healthbarCanvasInjector.SetDependency(this);
        }

        public void CreateNewHealthbar(IHealthbarData entityHealth)
        {
            var healthbar = Instantiate(_healthbarPrefab, transform);
            healthbar.SetHealth(entityHealth);
        }

        // void CreatePlayerHealthbar()
        // {
        //     _playerHealthbar = Instantiate(_healthbarPrefab, transform);
        //     _playerHealthbar.SetHealth(_gameObjects.player.body);
        //     _playerHealthbar.SetColor(_playerHealthbarColor);
        // }
    }
}