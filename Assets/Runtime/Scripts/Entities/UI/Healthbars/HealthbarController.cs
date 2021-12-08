using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Entities.UI
{
    public class HealthbarController : MonoBehaviour
    {
        [SerializeField] Healthbar _healthbarPrefab;
        [SerializeField] GameObjects _gameObjects;
        [SerializeField] Color _playerHealthbarColor = Color.red;
        [SerializeField] Color _enemyHealthbarColor = Color.red;

        Healthbar _playerHealthbar;

        public void Init()
        {
            Health.OnHealthInit += CreateNewHealthbar;
            CreatePlayerHealthbar();
        }

        void OnDestroy()
        {
            Health.OnHealthInit -= CreateNewHealthbar;
        }

        void CreateNewHealthbar(IHealthbarData entityHealth)
        {
            var healthbar = Instantiate(_healthbarPrefab, transform);
            healthbar.SetHealth(entityHealth);
            healthbar.SetColor(_enemyHealthbarColor);
        }

        void CreatePlayerHealthbar()
        {
            _playerHealthbar = Instantiate(_healthbarPrefab, transform);
            _playerHealthbar.SetHealth(_gameObjects.player.body);
            _playerHealthbar.SetColor(_playerHealthbarColor);
        }
    }
}