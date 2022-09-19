using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Rng = System.Random;

namespace Map
{
    public class TileStorage
    {
        List<Vector3> _storage;
        int _count;

        public bool isEmpty => _count == 0;
		public int count => _count;

        public TileStorage()
        {
            _storage = new List<Vector3>();
        }

        public TileStorage(int capacity)
        {
            _storage = new List<Vector3>(capacity);
        }

        public void Push(Vector3 tile)
        {
            if (_storage.Count == _count)
            {
                _storage.Add(tile);
            }
            else
            {
                _storage[_count] = tile;
            }
            _count++;
        }

        public bool Pull(Rng rng, out Vector3 tile)
        {
			tile = Vector3.zero;
			if(_count == 0) return false;

			int index = rng.Next(_count);
			tile = _storage[index];
			_count--;

			if(_count == 0 || index == _count) return true;
			//move last element to position
			_storage[index] = _storage[_count];


			return true;
        }
    }
}

