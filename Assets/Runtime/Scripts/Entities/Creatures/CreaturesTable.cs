using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rng = System.Random;


namespace Entities
{
    [CreateAssetMenu(fileName ="CreaturesTable", menuName ="Entities/Creatures Table")]
    public class CreaturesTable : ScriptableObject, IDataListSource<CreatureTemplate>
    {
        [SerializeField] bool _getOnlyOneElenemt;
        [Range(0, 1)]
        [SerializeField] float _chanceOfNone;

        [SerializeField] CreaturesTable[] _childCreatureTables;
        [SerializeField] CreatureData[] _creatures;

        public DataListGenerator<CreatureTemplate> dataListGenerator { get; private set; }
        
        IDataListSource<CreatureTemplate>[] IDataListSource<CreatureTemplate>.childTables => _childCreatureTables;
        IDataCount<CreatureTemplate>[] IDataListSource<CreatureTemplate>.dataItems => _creatures;
        bool IDataListSource<CreatureTemplate>.getOnlyOneElenemt => _getOnlyOneElenemt;
        float IDataListSource<CreatureTemplate>.chanceOfNone => _chanceOfNone;

        private void OnEnable()
        {
            if (dataListGenerator != null) return;
            dataListGenerator = new DataListGenerator<CreatureTemplate>(this);
        }

        public IEnumerable<CreatureTemplate> GetTemplates(Rng rng)
        {
            var creatures = new CreaturesList();
            dataListGenerator.FillDataList(rng, ref creatures);
            return creatures.creaturesList;
        }

        [ContextMenu("Check Generation")]
        void Generate()
        {
            var creatures = GetTemplates(new Rng());

            foreach (var itemSlot in creatures)
            {
                Debug.Log($"{itemSlot.name}");
            }
        }


        #region Supporting classes
        [System.Serializable]
        public class CreatureData : IDataCount<CreatureTemplate>
        {
            [SerializeField] CreatureTemplate _template;
            [SerializeField] int _count = 1;

            public CreatureTemplate element => _template;
            public int count => _count;
        }

        public class CreaturesList : IDataList<CreatureTemplate>
        {
            List<CreatureTemplate> _creaturesList = new List<CreatureTemplate>();
            public List<CreatureTemplate> creaturesList => _creaturesList;

            public void AddItems(CreatureTemplate item, int count)
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

