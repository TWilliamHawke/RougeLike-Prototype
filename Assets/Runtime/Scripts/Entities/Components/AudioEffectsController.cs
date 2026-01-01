using UnityEngine;

namespace Entities
{
    public class AudioEffectsController : MonoBehaviour, IEntityComponent
    {
        [SerializeField] AudioSource _audioSource;

        int _selectedIndex = 0;

        public void PlaySound(AudioClip sound)
        {
            _audioSource.PlayOneShot(sound);
            IncrementSourceIndex();
        }

        public void IncrementSourceIndex()
        {
            _selectedIndex++;

            // if (_selectedIndex >= _audioSources.Length)
            // {
            //     _selectedIndex = 0;
            // }
        }
    }
}