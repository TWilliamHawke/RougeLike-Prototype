using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/AbilitySoundKit")]
    public class AbilitySoundKit : ScriptableObject
    {
        [SerializeField] List<AudioClip> _useSounds = new();
        [SerializeField] List<AudioClip> _failSounds = new();

        public AudioClip useSound => _useSounds.GetRandom();
        public AudioClip failSound => _failSounds.GetRandom();
    }
}