using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Entities.Combat
{
    public class Projectile : MonoBehaviour
    {
        ProjectileTemplate _template;
        [SerializeField] TMP_Text _TMPSprite;
        [SerializeField] AudioSource _audioSource;

        public ProjectileTemplate template => _template;
        public float speed => _template.speedMult;

        public void SetTemplate(ProjectileTemplate template)
        {
            _template = template;
            _TMPSprite.text = template.bodyChar;
            _TMPSprite.color = template.color;
            _TMPSprite.enabled = true;

        }

        public void PlayImpactSound()
        {
            PlaySound(_template.impactSounds.GetRandom());
        }

        public void PlayFireSound()
        {
            PlaySound(_template.fireSounds.GetRandom());
        }

        public void HideSprite() //but still play sound
        {
            _TMPSprite.enabled = false;
        }

        void PlaySound(AudioClip sound)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(sound);
        }

    }
}