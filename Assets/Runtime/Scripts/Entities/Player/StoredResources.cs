using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Player
{
	[CreateAssetMenu(fileName ="StoredResourses", menuName ="Musc/StoredResourses")]
	public class StoredResources : ScriptableObject
	{
	    [SerializeField] ResourceData[] _startResources;

		Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();
		Dictionary<ResourceType, Resource> _types = new Dictionary<ResourceType, Resource>();

		public event UnityAction<ResourceType> OnResourceChange;

		public int this[ResourceType type] => _resources[type];
		public int gold => _resources[ResourceType.gold];

		public void Init()
        {
            SetStartResources();
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
			int maxCount = _types[type].maxCount;
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
			if(_resources[type] < count) return false;

			SpendResource(type, count);
			return true;
		}

		public int GetResource(ResourceType type)
		{
			return _resources[type];
		}

        private void SetStartResources()
        {
            foreach (var data in _startResources)
            {
                _resources[data.resource.type] = data.count;
                _types[data.resource.type] = data.resource;
            }
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
		gold,
		magicDust
	}
}