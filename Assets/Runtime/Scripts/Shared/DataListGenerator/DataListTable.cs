using System.Collections.Generic;
using UnityEngine;

public abstract class DataListTable<T> : ScriptableObject
{
    [SerializeField] DataListSelectionModes _selectionMode;

    protected abstract IEnumerable<IDataListElementSource<T>> childTables { get; }
    protected abstract IEnumerable<IDataListElementSource<T>> childElements { get; }

    public IEnumerable<IDataListElement<T>> GetElements()
    {
        if (_selectionMode == DataListSelectionModes.Random)
        {
            return GetRandomElements();
        }
        else
        {
            return GetAllElements();
        }
    }

    private IEnumerable<IDataListElementSource<T>> GetDataListSources()
    {
        foreach (var table in childTables)
        {
            yield return table;
        }

        foreach (var item in childElements)
        {
            yield return item;
        }
    }

    private IEnumerable<IDataListElement<T>> GetRandomElements()
    {
        var elements = GetDataListSources();

        var selectedElement = elements.GetRandonByWeight(el => el.weight);
        if (selectedElement == null) yield break;

        foreach (var element in selectedElement.GetElements())
        {
            yield return element;
        }
    }

    private IEnumerable<IDataListElement<T>> GetAllElements()
    {
        var sources = GetDataListSources();

        foreach (var source in sources)
        {
            foreach(var element in source.GetElements())
            {
                yield return element;
            }
        }
    }
}

