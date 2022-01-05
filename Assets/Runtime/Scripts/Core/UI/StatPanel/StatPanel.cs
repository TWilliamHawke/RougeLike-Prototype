using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using Entities;
using UnityEngine;

namespace Core.UI
{
    public class StatPanel : MonoBehaviour
    {
        [SerializeField] PlayerStats _playerStats;
        [SerializeField] GameObjects _gameObjects;
        [SerializeField] StatBar _healthbar;
        [SerializeField] StatBar _manabar;

        Health _playerHealth;

        public void Init()
        {
            _playerHealth = _gameObjects.player.GetComponent<Health>();
            _playerHealth.OnHealthChange += UpdateHealthbar;
            _playerStats.OnManaChange += UpdateManabar;

			UpdateManabar();
			UpdateHealthbar();
        }

        void OnDestroy()
        {
            _playerStats.OnManaChange += UpdateManabar;
        }



        void UpdateHealthbar()
        {
			_healthbar.ChangeStat(_playerHealth.currentHealth, _playerHealth.maxHealth);
        }

        void UpdateManabar()
        {
			_manabar.ChangeStat(_playerStats.curentMana, _playerStats.maxMana);
        }

    }
}