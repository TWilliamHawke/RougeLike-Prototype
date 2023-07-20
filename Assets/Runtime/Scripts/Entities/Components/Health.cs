using System.Collections;
using System.Collections.Generic;
using Entities.Behavior;
using Entities.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Health : MonoBehaviour, IHealthComponent, IHealthbarData, IInjectionTarget
    {
        public event UnityAction OnHealthChange;

        [SerializeField] Body _body;
        [SerializeField] Injector _healthbarCanvasInjector;

        [InjectField] HealthbarCanvas _healthbarCanvas;

        IHaveHealthData _healthData;

        int _currentHealth;

        public int currentHealth => _currentHealth;
        public int maxHealth => _healthData?.maxHealth ?? 100;
        public Vector3 bodyPosition => _body.transform.position;

        public bool waitForAllDependencies => false;

        public BehaviorType behavior { get; set; } = BehaviorType.none;

        void Awake()
        {
            _healthData = GetComponent<IHaveHealthData>();
            _healthbarCanvasInjector.AddInjectionTarget(this);
        }

        public void Init(IHaveHealthData entitiy)
        {
            _currentHealth = _healthData.maxHealth;
            behavior = entitiy.faction.GetAntiPlayerBehavior();
            entitiy.OnFactionChange += UpdateFaction;
            OnHealthChange?.Invoke();
        }

        public void FillToMax()
        {
            _currentHealth = maxHealth;
            OnHealthChange?.Invoke();
        }

        public void RestoreHealth(int health)
        {
            ChangeHealth(health);
        }

        public void DamageHealth(int health)
        {
            ChangeHealth(-health);
        }

        void UpdateFaction(Faction newFaction)
        {
            behavior = newFaction.GetAntiPlayerBehavior();
            OnHealthChange?.Invoke();
        }

        void ChangeHealth(int health)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + health, 0, maxHealth);

            OnHealthChange?.Invoke();

            if (_currentHealth == 0 && _healthData.deathSounds.Length > 0)
            {
                var sound = _healthData.deathSounds.GetRandom();
                _body.PlaySound(sound);
            }
        }

        void IInjectionTarget.FinalizeInjection()
        {
            _healthbarCanvas.CreateNewHealthbar(this);
            OnHealthChange?.Invoke();
        }
    }
}