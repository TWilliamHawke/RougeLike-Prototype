using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class SoundKit : ScriptableObject
    {
        [SerializeField] AudioClip[] _attackSounds;
        [SerializeField] AudioClip[] _deathSounds;

        public AudioClip[] attackSounds => _attackSounds;
        public AudioClip[] deathSounds => _deathSounds;

    }
}