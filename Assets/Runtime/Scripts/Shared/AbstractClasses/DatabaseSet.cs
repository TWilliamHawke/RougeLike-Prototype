using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DatabaseSet : ScriptableObject
{
    public abstract bool Has(ScriptableObject element);
}

public abstract class DatabaseSet<T> : DatabaseSet, IEnumerable<T> where T : ScriptableObject
{
    [SerializeField] T[] _elements;

    HashSet<T> _elementsSet;

    public void Add(T element)
    {
        Init();
        _elementsSet.Add(element);
    }

    public bool Has(T element)
    {
        Init();
        return _elementsSet.Contains(element);
    }

    public override bool Has(ScriptableObject element)
    {
        if (element is T typedElement)
        {
            return Has(typedElement);
        }
        else
        {
            return false;
        }
    }

    public void Remove(T element)
    {
        Init();
        _elementsSet.Remove(element);
    }

    private void Init()
    {
        if (_elementsSet is not null) return;
        _elementsSet = new(_elements.Length);
        _elements.ForEach(element => _elementsSet.Add(element));
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return _elementsSet.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _elementsSet.GetEnumerator();
    }
}
