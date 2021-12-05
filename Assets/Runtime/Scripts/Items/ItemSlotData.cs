using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	[System.Serializable]
	public class ItemSlotData<T> where T : Item
	{
		T _item;
		int _count;

        public T item => _item;
		public int count => _count;

        public ItemSlotData(T item)
        {
            _item = item;
            _count = 1;
        }

        public ItemSlotData(T item, int count)
        {
            _item = item;
            _count = count;
        }


		public void AddToStack()
		{
			_count++;
		}

		public void RemoveFromStack()
        {
            if (count == 0) return;
            _count--;
        }

		public void IncreaseCountBy(int num)
		{
			_count += num;
		}

		public void DecreaseCountBy(int num)
		{
			_count = Mathf.Max(0, _count - num);
		}

		public void FillToMaxSize()
		{
			_count = _item.maxStackSize;
		}
    }
}