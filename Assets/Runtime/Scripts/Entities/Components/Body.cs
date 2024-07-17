using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    [RequireComponent(typeof(AudioSource))]
    public class Body : MonoBehaviour, IAudioSource, IHavePosition
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] TMP_Text _TMPSprite;
        [SerializeField] TMP_Text _remains;
        [SerializeField] [Range(0, 1)] float _deathAnimationSpeed = .5f;

        public event UnityAction OnAnimationEnd;
        public Vector3 position => transform.position;

        enum AnimationState {
            death = -1,
            none = 0,
            raise = 1,
        }

        float _animationProgress = 0;
        AnimationState _animationState = AnimationState.none;
        Vector3 _defaultPosition;

        public void UpdateSkin(string bodyChar, Color color)
        {
            _TMPSprite.text = bodyChar;
            _TMPSprite.color = color;
        }

        public void PlaySound(AudioClip sound)
        {
            _audioSource.PlayOneShot(sound);
        }

        public void StartDeathAnimation()
        {
            _animationState = AnimationState.death;
            _defaultPosition = _TMPSprite.rectTransform.position;
        }


        private void Update() {
            if (_animationState == AnimationState.none) return;
            _animationProgress += Time.deltaTime * _deathAnimationSpeed;

            _TMPSprite.rectTransform.position = _defaultPosition + Vector3.up
                * _animationProgress *(int)_animationState;

            if (_animationState == AnimationState.death) {
                var color = new Color(1, 1, 1, Mathf.Clamp01(_animationProgress));
                _remains.color = color;
            }

            if (_animationProgress >= 1) {
                OnAnimationEnd?.Invoke();
                _animationProgress = 0;
                _animationState = AnimationState.none;
            }

        }



    }
}