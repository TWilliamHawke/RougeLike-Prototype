using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Abilities
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

        public void MoveTo(Vector3 position)
        {
            transform.position = position;
        }

        public void RotateTo(Vector3 direction)
        {
            transform.right = direction;
        }

        public void PlayFireSound()
        {
            PlaySound(_template.fireSound);
        }

        public void HideSprite()
        {
            _TMPSprite.enabled = false;
        }

        private void PlaySound(AudioClip sound)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(sound);
        }

    }
}