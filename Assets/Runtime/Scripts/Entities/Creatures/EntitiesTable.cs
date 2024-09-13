using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rng = System.Random;


namespace Entities
{
    [CreateAssetMenu(fileName ="CreaturesTable", menuName ="Entities/Creatures Table")]
    public class EntitiesTable : ScriptableObject, IDataListSource<EntityTemplate>
    {
        [SerializeField] bool _getOnlyOneElenemt;
        [Range(0, 1)]
        [SerializeField] float _chanceOfNone;

        [SerializeField] EntitiesTable[] _childTables;
        [SerializeField] EntityData[] _entities;

        public DataListGenerator<EntityTemplate> dataListGenerator { get; private set; }
        
        IDataListSource<EntityTemplate>[] IDataListSource<EntityTemplate>.childTables => _childTables;
        IDataCount<EntityTemplate>[] IDataListSource<EntityTemplate>.dataItems => _entities;
        bool IDataListSource<EntityTemplate>.getOnlyOneElenemt => _getOnlyOneElenemt;
        float IDataListSource<EntityTemplate>.chanceOfNone => _chanceOfNone;

        private void OnEnable()
        {
            if (dataListGenerator != null) return;
            dataListGenerator = new DataListGenerator<EntityTemplate>(this);
        }

        public IEnumerable<EntityTemplate> GetTemplates(Rng rng)
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
        public class EntityData : IDataCount<EntityTemplate>
        {
            [SerializeField] EntityTemplate _template;
            [PlusMinusBtn]
            [SerializeField] int _count = 1;

            public EntityTemplate element => _template;
            public int count => _count;
        }

        public class CreaturesList : IDataList<EntityTemplate>
        {
            List<EntityTemplate> _creaturesList = new List<EntityTemplate>();
            public List<EntityTemplate> creaturesList => _creaturesList;

            public void AddItems(EntityTemplate item, int count)
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

