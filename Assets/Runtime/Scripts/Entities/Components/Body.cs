using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    [RequireComponent(typeof(AudioSource))]
    public class Body : MonoBehaviour, IHealthbarData, IAudioSource
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] TMP_Text _TMPSprite;

        IHealthComponent _health;

        Transform IHealthbarData.transform => transform;
        int IHealthbarData.maxHealth => _health.maxHealth;
        int IHealthbarData.currentHealth => _health.currentHealth;
        Color IHealthbarData.healthbarColor => _health.healthbarColor;

        public event UnityAction OnHealthChange;

        public void Init(IHealthComponent healthComponent)
        {
            _health = healthComponent;
            _health.OnHealthChange += InvokeEvent;
        }

        public void UpdateSkin(string bodyChar, Color color)
        {
            _TMPSprite.text = bodyChar;
            _TMPSprite.color = color;
        }

        public void PlaySound(AudioClip sound)
        {
            _audioSource.PlayOneShot(sound);
        }

        void OnDestroy()
        {
            if (_health is null) return;
            _health.OnHealthChange -= InvokeEvent;
        }

        void InvokeEvent()
        {
            OnHealthChange?.Invoke();
        }

    }
}