using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using Entities;
using UnityEngine;

namespace Core.UI
{
    public class StatPanel : MonoBehaviour, IInjectionTarget
    {
        [SerializeField] PlayerStats _playerStats;
        [SerializeField] Injector _playerInjector;
        [SerializeField] StatBar _healthbar;
        [SerializeField] StatBar _manabar;

        [InjectField] Player _player;

        Health _playerHealth;

        public bool waitForAllDependencies => false;

        public void FinalizeInjection()
        {
            _playerHealth = _player.GetComponent<Health>();
            _playerHealth.OnHealthChange += UpdateHealthbar;
			UpdateHealthbar();
        }

        public void Init()
        {
            _playerInjector.AddInjectionTarget(this);

			UpdateManabar();
        }

        void OnDestroy()
        {
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