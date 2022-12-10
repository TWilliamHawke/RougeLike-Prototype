using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Combat;

namespace Entities
{
    [CreateAssetMenu(fileName = "Resists", menuName = "Entities/ResistSet")]
    public class ResistSet : ScriptableObject
    {
        [SerializeField] int _physical;
        [SerializeField] int _fire;
        [SerializeField] int _poison;
        [SerializeField] int _electric;
        [SerializeField] int _frost;

        Dictionary<DamageType, int> _set;

        public Dictionary<DamageType, int> set => _set;

        void OnEnable()
        {
			_set = new Dictionary<DamageType, int>(5);
			_set.Add(DamageType.physical, _physical);
			_set.Add(DamageType.fire, _fire);
			_set.Add(DamageType.poison, _poison);
			_set.Add(DamageType.frost, _frost);
			_set.Add(DamageType.electric, _electric);
        }
    }
}