using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rng = System.Random;


namespace Entities
{
    [CreateAssetMenu(fileName ="CreaturesTable", menuName ="Entities/Creatures Table")]
    public class CreaturesTable : ScriptableObject, IDataListSource<EnemyTemplate>
    {
        [SerializeField] bool _getOnlyOneElenemt;
        [Range(0, 1)]
        [SerializeField] float _chanceOfNone;

        [SerializeField] CreaturesTable[] _childCreatureTables;
        [SerializeField] CreatureData[] _creatures;
        DataListGenerator<EnemyTemplate> _dataListGenerator;

        DataListGenerator<EnemyTemplate> IDataListSource<EnemyTemplate>.dataListGenerator => _dataListGenerator;
        IDataListSource<EnemyTemplate>[] IDataListSource<EnemyTemplate>.childTables => _childCreatureTables;
        IDataCount<EnemyTemplate>[] IDataListSource<EnemyTemplate>.dataItems => _creatures;
        bool IDataListSource<EnemyTemplate>.getOnlyOneElenemt => _getOnlyOneElenemt;
        float IDataListSource<EnemyTemplate>.chanceOfNone => _chanceOfNone;

        private void OnEnable()
        {
            if (_dataListGenerator != null) return;
            _dataListGenerator = new DataListGenerator<EnemyTemplate>(this);
        }

        public IEnumerable<EnemyTemplate> GetCreatures(Rng rng)
        {
            var creatures = new CreaturesList();
            _dataListGenerator.FillDataList(rng, ref creatures);
            return creatures.creaturesList;
        }

        [ContextMenu("Check Generation")]
        void Generate()
        {
            var creatures = GetCreatures(new Rng());

            foreach (var itemSlot in creatures)
            {
                Debug.Log($"{itemSlot.name}");
            }
        }


        #region Supporting classes
        [System.Serializable]
        public class CreatureData : IDataCount<EnemyTemplate>
        {
            [SerializeField] EnemyTemplate _template;
            [SerializeField] int _count = 1;

            public EnemyTemplate item => _template;
            public int count => _count;
        }

        public class CreaturesList : IDataList<EnemyTemplate>
        {
            List<EnemyTemplate> _creaturesList = new List<EnemyTemplate>();
            public List<EnemyTemplate> creaturesList => _creaturesList;

            public void AddItems(EnemyTemplate item, int count)
            {
                if (count <= 0) return;

                for (int i = 0; i < count; i++)
                {
                    _creaturesList.Add(item);
                }
            }
        }
        #endregion
    }


}

