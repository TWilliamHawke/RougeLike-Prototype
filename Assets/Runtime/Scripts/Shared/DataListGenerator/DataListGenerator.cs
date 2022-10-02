using System.Collections.Generic;
using UnityEngine;

public abstract class DataListGenerator<T> : ScriptableObject
{
    [SerializeField] bool _getOnlyOneElenemt;
    [Range(0, 1)]
    [SerializeField] float _chanceOfNone;

    protected abstract DataListGenerator<T>[] childTables { get; }
    protected abstract DataCount<T>[] dataItems { get; }

    //public abstract IDataList<T> GetLoot();

    public void FillDataList<U>(ref U loot) where U : IDataList<T>
    {
        if (Random.Range(0f, 1f) < _chanceOfNone) return;

        if (_getOnlyOneElenemt)
        {
            GetRandomItems(ref loot);
        }
        else
        {
            GetAllItems(ref loot);
        }
    }

    [ContextMenu("Check Generation")]
    protected abstract void Generate();

    [ContextMenu("CheckChildren")]
    public void CheckErrors()
    {
        var set = new HashSet<DataListGenerator<T>>();

        CheckTableInSet(ref set);
    }

    void CheckTableInSet(ref HashSet<DataListGenerator<T>> set)
    {
        if (set.Contains(this))
        {
            throw new DataListGeneratorException<T>(this);
        }
        else
        {
            set.Add(this);
            foreach (var lootTable in childTables)
            {
                lootTable.CheckTableInSet(ref set);
            }
            set.Remove(this);
        }
    }

    private void GetRandomItems<U>(ref U loot) where U : IDataList<T>
    {
        int variantsCount = childTables.Length + dataItems.Length;
        if (variantsCount == 0) return;

        int index = Random.Range(0, childTables.Length + dataItems.Length);

        if (childTables.Length > 0 && index < childTables.Length)
        {
            childTables[index].FillDataList(ref loot);
        }
        else
        {
            var itemSlot = dataItems[index];
            loot.AddItems(itemSlot.item, itemSlot.count);
        }

    }

    private void GetAllItems<U>(ref U loot) where U : IDataList<T>
    {
        foreach (var lootTable in childTables)
        {
            lootTable.FillDataList(ref loot);
        }

        foreach (var itemSlot in dataItems)
        {
            loot.AddItems(itemSlot.item, itemSlot.count);
        }
    }
}

