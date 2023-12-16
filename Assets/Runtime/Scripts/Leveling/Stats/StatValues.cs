using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Stats
{
    [CreateAssetMenu(fileName = "StatValues", menuName = "Entities/Stat Values")]
    public class StatValues : ScriptableObject, IEnumerable<StatValue>
    {
        [SerializeField]
        List<StatValue> _statValues;

        public IEnumerator<StatValue> GetEnumerator()
        {
            return _statValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _statValues.GetEnumerator();
        }
    }

    [System.Serializable]
    public class StatValue
    {
        public Stat stat;
        public int value;
    }
}
