using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    [RequireComponent(typeof(AudioSource))]
    public class Body : MonoBehaviour, IHealthbarData
    {
        [SerializeField] AudioSource _audioSource;

        IHealthComponent _health;

        Transform IHealthbarData.transform => transform;
        int IHealthbarData.maxHealth => _health.maxHealth;
        int IHealthbarData.currentHealth => _health.currentHealth;

        public event UnityAction OnHealthChange;

        public void Init(IHealthComponent healthComponent)
        {
            _health = healthComponent;
            _health.OnHealthChange += InvokeEvent;
        }

        public void PlaySound(AudioClip sound)
        {
            _audioSource.PlayOneShot(sound);
        }

        void OnDestroy()
        {
            _health.OnHealthChange -= InvokeEvent;
        }

        void InvokeEvent()
        {
            OnHealthChange?.Invoke();
        }

    }
}