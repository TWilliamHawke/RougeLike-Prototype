using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "AttackTargetPattern", menuName = "Abilities/AttackPattern")]
    public class AttackPatterns : AOEPatterns
    {
        Dictionary<Vector3, List<Vector3>> _patterns;

        static List<Matrix3x3Lite> _rotationMatrixes = new()
        {
            new Matrix3x3Lite(Vector3.right, Vector3.up),
            new Matrix3x3Lite(Vector3.down, Vector3.right),
            new Matrix3x3Lite(Vector3.left, Vector3.down),
            new Matrix3x3Lite(Vector3.up, Vector3.left),
        };

        void OnEnable()
        {
            CreatePatterns();
        }

        public void CreatePatterns()
        {
            _patterns = new();
            foreach (var patternData in rawPatterns)
            {
                foreach (var matrix in _rotationMatrixes)
                {
                    var rotatedTargets = patternData.targets
                        .Select(target => target * matrix)
                        .ToList();
                    var hitPosition = patternData.hitPosition * matrix;
                    _patterns[hitPosition] = rotatedTargets;
                }
            }
        }

        public bool HitPositionIsValid(Vector3Int hitPosition)
        {
            return _patterns.ContainsKey(hitPosition);
        }

        public IEnumerable<Vector3Int> GetTargetPositions(Vector3 hitPosition)
        {
            if (_patterns.TryGetValue(hitPosition, out var pattern))
            {
                foreach(var pos in pattern)
                {
                    yield return pos.ToInt();
                }
            }
        }
    }
}