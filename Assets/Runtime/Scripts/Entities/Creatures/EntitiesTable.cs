using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(fileName = "CreaturesTable", menuName = "Entities/Creatures Table")]
    public class EntitiesTable : DataListTable<EntityTemplate>
    {
        [SerializeField] List<EntityTableData> _tables = new();
        [SerializeField] List<EntityData> _entities = new();

        protected override IEnumerable<IDataListElementSource<EntityTemplate>> childTables => _tables;
        protected override IEnumerable<IDataListElementSource<EntityTemplate>> childElements => _entities;

        public IEnumerable<EntityTemplate> GetTemplates()
        {
            var templatesList = GetElements();
            foreach (var templateData in templatesList)
            {
                for (int i = 0; i < templateData.count; i++)
                {
                    yield return templateData.element;
                }
            }
        }

        [ContextMenu("Check Generation")]
        void Generate()
        {
            var templates = GetTemplates();

            foreach (var tempplate in templates)
            {
                Debug.Log($"{tempplate.name}");
            }
        }

        #region Supporting classes
        [System.Serializable]
        public class EntityData : IDataListElementSource<EntityTemplate>
        {
            [SerializeField] EntityTemplate _template;
            [SerializeField] IntValue _count = 1;
            [PlusMinusBtn]
            [SerializeField] int _weight = 1;

            public EntityTemplate element => _template;
            public int count => _count;
            public int weight => _weight;

            public IEnumerable<IDataListElement<EntityTemplate>> GetElements()
            {
                yield return new DataListElement<EntityTemplate>
                {
                    element = _template,
                    count = _count
                };
            }
        }

        [System.Serializable]
        public class EntityTableData : IDataListElementSource<EntityTemplate>
        {
            [SerializeField] EntitiesTable _table;
            [PlusMinusBtn]
            [SerializeField] IntValue _count = 1;
            [PlusMinusBtn]
            [SerializeField] int _weight = 1;

            public int weight => _weight;

            public IEnumerable<IDataListElement<EntityTemplate>> GetElements()
            {
                for (int i = 0; i < _count.minValue; i++)
                {
                    foreach (var element in _table.GetElements())
                    {
                        yield return element;
                    }
                }
            }
        }
        #endregion
    }
}

