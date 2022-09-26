using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class StoredResources : IItemSection
    {
        Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();
        Dictionary<ResourceType, Resource> _types = new Dictionary<ResourceType, Resource>();

        public event UnityAction<ResourceType> OnResourceChange;

        public int this[ResourceType type] => _resources[type];
        public int gold => _resources[ResourceType.gold];

        public StoredResources(Resource[] resources)
        {
            SetStartResources(resources);
        }

        public void AddGold(int count)
        {
            AddResource(ResourceType.gold, count);
        }

        public void SpendGold(int count)
        {
            SpendResource(ResourceType.gold, count);
        }

        public void AddResource(ResourceType type, int count)
        {
            if (type == ResourceType.none) return;

            int maxCount = _types[type].maxStackSize;
            _resources[type] = Mathf.Min(_resources[type] + count, maxCount);
            OnResourceChange?.Invoke(type);
        }

        public void SpendResource(ResourceType type, int count)
        {
            _resources[type] = Mathf.Max(_resources[type] - count, 0);
            OnResourceChange?.Invoke(type);
        }

        public bool TrySpendResource(ResourceType type, int count)
        {
            if (_resources[type] < count) return false;

            SpendResource(type, count);
            return true;
        }

        public int GetResource(ResourceType type)
        {
            return _resources[type];
        }

        private void SetStartResources(Resource[] resources)
        {
            foreach (var resource in resources)
            {
                var type = resource.type;
                _resources[type] = resource.startCount;
                _types[type] = resource;
            }
        }




        void IItemSection.AddItem(Item item)
        {
            AddItems(item, 1);
        }

        public void AddItems(Item item, int count)
        {
            AddResource((item as Resource)?.type ?? ResourceType.none, count);
        }

        bool IItemSection.ItemMeet(Item item)
        {
            return item is Resource;
        }

        void IItemSection.Clear()
        {
        }

        int IItemSection.FindItemCount(Item item)
        {
            var type = (item as Resource)?.type ?? ResourceType.none;
            return _resources[type];
        }

        [System.Serializable]
        class ResourceData
        {
            public Resource resource;
            public int count;
        }
    }

    public enum ResourceType
    {
        none,
        gold,
        magicDust
    }
}