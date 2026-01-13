using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rng = System.Random;


namespace Entities
{
    [CreateAssetMenu(fileName ="CreaturesTable", menuName ="Entities/Creatures Table")]
    public class EntitiesTable : ScriptableObject, IDataListTable<EntityTemplate>
    {
        [SerializeField] bool _getOnlyOneElenemt;
        [Range(0, 1)]
        [SerializeField] float _chanceOfNone;

        [SerializeField] EntitiesTable[] _childTables;
        [SerializeField] EntityData[] _entities;

        public DataListGenerator<EntityTemplate> dataListGenerator { get; private set; }
        
        IDataListTable<EntityTemplate>[] IDataListTable<EntityTemplate>.childTables => _childTables;
        IDataListElement<EntityTemplate>[] IDataListTable<EntityTemplate>.dataItems => _entities;
        bool IDataListTable<EntityTemplate>.getOnlyOneElenemt => _getOnlyOneElenemt;
        float IDataListTable<EntityTemplate>.chanceOfNone => _chanceOfNone;

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
        public class EntityData : IDataListElement<EntityTemplate>
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

            public void AddElements(IDataListElement<EntityTemplate> elements)
            {
                if (elements.count <= 0) return;

                for (int i = 0; i < elements.count; i++)
                {
                    _creaturesList.Add(elements.element);
                }
            }
        }
        #endregion
    }


}

