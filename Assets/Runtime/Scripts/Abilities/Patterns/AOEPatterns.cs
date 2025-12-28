using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "AOEPatterns", menuName = "Abilities/AOEPatterns")]
    public class AOEPatterns : ScriptableObject
    {
        [SerializeField] List<AttackPatternData> _rawPatterns = new();

        public List<AttackPatternData> rawPatterns => _rawPatterns;

    }
}