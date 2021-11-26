using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

namespace Entities.UI
{
    public class HealthbarController : MonoBehaviour
    {
        [SerializeField] Healthbar _healthbarPrefab;
        [SerializeField] PlayerStats _playerStats;
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

        void CreateNewHealthbar(Health entityHealth)
        {
            var healthbar = Instantiate(_healthbarPrefab, transform);
            healthbar.SetHealth(entityHealth);
            healthbar.SetColor(_enemyHealthbarColor);
        }

        void CreatePlayerHealthbar()
        {
            _playerHealthbar = Instantiate(_healthbarPrefab, transform);
            _playerHealthbar.SetHealth(_playerStats);
            _playerHealthbar.SetColor(_playerHealthbarColor);
        }
    }
}