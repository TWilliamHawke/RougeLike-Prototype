using UnityEngine;

namespace Entities
{
    public class AudioEffectsController : MonoBehaviour, IEntityComponent, IAudioController
    {
        [SerializeField] AudioSource[] _audioSources;

        int _selectedIndex = 0;

        public void PlaySound(AudioClip sound)
        {
            _audioSources[_selectedIndex].PlayOneShot(sound);
            IncrementSourceIndex();
        }

        public void IncrementSourceIndex()
        {
            _selectedIndex++;

            if (_selectedIndex >= _audioSources.Length)
            {
                _selectedIndex = 0;
            }
        }
    }

}
    public interface IAudioController
    {
        void PlaySound(AudioClip sound);
    }