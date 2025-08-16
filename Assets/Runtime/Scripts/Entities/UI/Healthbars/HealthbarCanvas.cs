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
        [InjectField] Player _player;

        Dictionary<Entity, Healthbar> _healthbars = new();
        Healthbar _playerHealthbar;

        private void Awake()
        {
            _playerHealthbar = Instantiate(_healthbarPrefab, transform);
            _healthbarCanvasInjector.SetDependency(this);
        }

        public void AddToObserve(Entity target)
        {
            var statsContainer = target.GetComponent<StatsContainer>();
            var healthStorage = statsContainer.FindStorage(_statList.health);

            var healthbar = Instantiate(_healthbarPrefab, transform);
            var controller = target.GetEntityComponent<PositionController>();
            healthbar.Observe(controller);
            healthbar.AddToObserve(healthStorage);
            healthbar.AddToObserve(target.GetEntityComponent<IFactionMember>());

            _healthbars.Add(target, healthbar);
        }

        public void RemoveFromObserve(Entity target)
        {
            _healthbars.Remove(target);
        }

        //Used in Unity Editor
        public void ObserveEntities()
        {
            _entitiesManager.AddObserver(this);
            _playerHealthbar.Observe(_player.GetEntityComponent<PositionController>());
        }

        //Used in Unity Editor
        public void CreatePlayerHealthbar()
        {
            var health = _playerStats.FindStorage(_statList.health);
            _playerHealthbar.AddToObserve(health);
        }
    }
}