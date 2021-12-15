using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(fileName = "Sounds", menuName ="Entities/SoundKit")]
    public class CreatureSoundKit : ScriptableObject, IHaveInjureSounds
    {
        [SerializeField] AudioClip[] _attackSounds;
        [SerializeField] AudioClip[] _deathSounds;

        public AudioClip[] attackSounds => _attackSounds;
        public AudioClip[] deathSounds => _deathSounds;

    }
}