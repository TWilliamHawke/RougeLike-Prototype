using System.Collections.Generic;
using Rng = System.Random;

//collection that return random element
public class RandomStack<T>
{
    List<T> _storage;
    int _count;

    public bool isEmpty => _count == 0;
    public int count => _count;

    public RandomStack()
    {
        _storage = new List<T>();
    }

    public RandomStack(int capacity)
    {
        _storage = new List<T>(capacity);
    }

    public void Push(T tile)
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

    public bool TryPull(Rng rng, out T tile)
    {
        tile = default;
        if (_count == 0) return false;

        int index = rng.Next(_count);
        tile = _storage[index];
        _count--;

        if (_count == 0 || index == _count) return true;
        //move last element to position
        _storage[index] = _storage[_count];

        return true;
    }
}


