using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class CreaturesTable : ScriptableObject
    {
        [SerializeField] bool _getOnlyOneElenemt;
        [Range(0, 1)]
        [SerializeField] float _chanceOfNone;

        [SerializeField] CreaturesTable[] _childLootTables;
        [SerializeField] CreatureData[] _creatures;

    }

    [System.Serializable]
    public class CreatureData
    {
        [SerializeField] EnemyTemplate _template;
        [SerializeField] int _count = 1;

        public EnemyTemplate template => _template;
        public int count => _count;

    }

}

