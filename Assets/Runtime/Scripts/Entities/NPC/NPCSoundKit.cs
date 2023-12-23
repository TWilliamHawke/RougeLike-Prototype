using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.NPC
{
    [CreateAssetMenu(fileName = "NPCSoundKit", menuName = "Entities/NPC Sound Kit", order = 0)]
    public class NPCSoundKit : ScriptableObject
    {
		[SerializeField] AudioClip[] _deathSounds;

		public AudioClip[] deathSounds => _deathSounds;
    }
}


