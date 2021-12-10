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

        public void SetTemplate(ProjectileTemplate template)
        {
            _template = template;
            _TMPSprite.text = template.bodyChar;
            _TMPSprite.color = template.color;
            _TMPSprite.enabled = true;

        }

        public void PlaySound(AudioClip sound)
        {
			_audioSource.Stop();
            _audioSource.PlayOneShot(sound);
        }

        public void Hide()
        {
            _TMPSprite.enabled = false;
        }

    }
}