using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    [RequireComponent(typeof(AudioSource))]
    public class Body : MonoBehaviour, IAudioSource
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] TMP_Text _TMPSprite;


        public void UpdateSkin(string bodyChar, Color color)
        {
            _TMPSprite.text = bodyChar;
            _TMPSprite.color = color;
        }

        public void PlaySound(AudioClip sound)
        {
            _audioSource.PlayOneShot(sound);
        }

    }
}