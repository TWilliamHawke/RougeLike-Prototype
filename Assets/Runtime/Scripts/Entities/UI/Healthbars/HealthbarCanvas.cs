using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using Entities.Stats;
using UnityEngine;

namespace Entities.UI
{
    public class HealthbarCanvas : MonoBehaviour, IObserver<Entity>
    {
        [SerializeField] Healthbar _healthbarPrefab;
        [SerializeField] Injector _healthbarCanvasInjector;
        [SerializeField] StatList _statList;
        [SerializeField] PlayerStats _playerStats;

        [InjectField] EntitiesManager _entitiesManager;

        Dictionary<Entity, Healthbar> _healthbars = new();
        Healthbar _playerHealthbar;

        private void Awake()
        {
            _healthbarCanvasInjector.SetDependency(this);
        }

        public void AddToObserve(Entity target)
        {
            var healthStorage = target.FindStatStorage(_statList.health);
            var health = target.GetEntityComponent<IHealthbarData>();

            var healthbar = Instantiate(_healthbarPrefab, transform);
            healthbar.BindHealth(health, healthStorage);
            healthbar.SubscribeOnFactionEvent(target.GetEntityComponent<IFactionMember>());

            _healthbars.Add(target, healthbar);
        }

        public void RemoveFromObserve(Entity target)
        {
            _healthbars.Remove(target);
        }

        public void AddObserver()
        {
            _entitiesManager.AddObserver(this);
        }

        public void CreatePlayerHealthbar()
        {
            var health = _playerStats.FindStorage(_statList.health);
            _playerHealthbar = Instantiate(_healthbarPrefab, transform);
            _playerHealthbar.BindHealth(_playerStats, health);
        }
    }
}