using System.Collections;
using System.Collections.Generic;
using Entities.Behavior;
using Entities.Combat;
using Entities.Stats;
using Entities.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Health : MonoBehaviour, IHealthComponent
    {

        [SerializeField] Injector _healthbarCanvasInjector;
        [SerializeField] StoredResource _health;

        ResourceStorage _healthStorage;

        public bool isDead => _healthStorage.currentValue <= 0;

        void Awake()
        {
            if(TryGetComponent<IEntityWithComponents>(out var entity))
            {
                entity.OnStatsInit += ObserveHealth;
            }
        }

        public void RestoreHealth(int value)
        {
            ChangeHealth(value);
        }

        public void DamageHealth(int value)
        {
            ChangeHealth(-value);
        }

        void ChangeHealth(int value)
        {
            _healthStorage.ChangeStat(value);
        }

        private void ObserveHealth(IStatsController controller)
        {
            _healthStorage = controller.FindStorage(_health);
        }

        private void GetTemplateData(ITemplateWithBaseStats template)
        {
            
        }
    }
}