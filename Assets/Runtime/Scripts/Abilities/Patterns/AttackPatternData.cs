using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Abilities
{
    [System.Serializable]
    public class AttackPatternData
    {
        public Vector3Int hitPosition => _hitPosition;
        public List<Vector3Int> targets => _targets;

        [SerializeField] List<Vector3Int> _targets = new();
        [SerializeField] Vector3Int _hitPosition;
        
        Dictionary<Vector3Int, int> _targetsCache;

        public Dictionary<Vector3Int, int> GetTargets()
        {
            CreateTargetsCache();
            return _targetsCache;
        }

        public void UpdateTargetsAndHit(Vector3Int position)
        {
            if (_targetsCache.ContainsKey(position))
            {
                if (_hitPosition == position)
                {
                    RemoveTarget(position);
                    _hitPosition = _targets.FirstOrDefault();
                }
                else
                {
                    _hitPosition = position;
                }
            }
            else
            {
                AddTarget(position);
                if (_targets.Count == 1)
                {
                    _hitPosition = position;
                }
            }
        }

        public void UpdateTargets(Vector3Int position)
        {
            if (_targetsCache.ContainsKey(position))
            {
                RemoveTarget(position);
            }
            else
            {
                AddTarget(position);
            }
        }

        private void AddTarget(Vector3Int position)
        {
            _targetsCache.Add(position, targets.Count);
            _targets = _targetsCache.Keys.ToList();
        }

        private void RemoveTarget(Vector3Int position)
        {
            _targetsCache.Remove(position);
            _targets = _targetsCache.Keys.ToList();
        }

        public void CreateTargetsCache()
        {
            if (_targetsCache != null) return;
            _targetsCache = new Dictionary<Vector3Int, int>(targets.Count);
            for (int i = 0; i < targets.Count; i++)
            {
                _targetsCache.Add(targets[i], i);
            }
        }
    }
}