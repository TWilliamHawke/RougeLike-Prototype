using System.Collections.Generic;
using UnityEngine;
using Rng = System.Random;


public class DataListGenerator<T>
{
    IDataListSource<T> dataListSource;
    Rng _rng;

    public DataListGenerator(IDataListSource<T> dataListSource)
    {
        this.dataListSource = dataListSource;
    }

    public void FillDataList<U>(Rng rng, ref U dataList) where U : IDataList<T>
    {
        _rng = rng;
        FillDataList(ref dataList);
    }


    public void FillDataList<U>(ref U dataList) where U : IDataList<T>
    {
        if (GetRandomChance() < dataListSource.chanceOfNone) return;

        if (dataListSource.getOnlyOneElenemt)
        {
            AddRandomItems(ref dataList);
        }
        else
        {
            AddAllItems(ref dataList);
        }
    }

    public void CheckErrors()
    {
        var set = new HashSet<DataListGenerator<T>>();
        CheckTableInSet(ref set);
    }

    private float GetRandomChance()
    {
        if (_rng is null) return Random.Range(0f, 1f);
        return (float)_rng.NextDouble();
    }

    private int GetRandomNumber(int min, int max)
    {
        if (_rng is null) return Random.Range(min, max);
        return _rng.Next(min, max);
    }

    private void CheckTableInSet(ref HashSet<DataListGenerator<T>> set)
    {
        if (set.Contains(this))
        {
            throw new DataListGeneratorException<T>(this);
        }
        else
        {
            set.Add(this);
            foreach (var lootTable in dataListSource.childTables)
            {
                lootTable.dataListGenerator.CheckTableInSet(ref set);
            }
            set.Remove(this);
        }
    }

    private void AddRandomItems<U>(ref U dataList) where U : IDataList<T>
    {
        int variantsCount = dataListSource.childTables.Length + dataListSource.dataItems.Length;
        if (variantsCount == 0) return;

        int index = GetRandomNumber(0, dataListSource.childTables.Length + dataListSource.dataItems.Length);

        if (dataListSource.childTables.Length > 0 && index < dataListSource.childTables.Length)
        {
            dataListSource.childTables[index].dataListGenerator.FillDataList(ref dataList);
        }
        else
        {
            var itemSlot = dataListSource.dataItems[index];
            dataList.AddItems(itemSlot.item, itemSlot.count);
        }
    }

    private void AddAllItems<U>(ref U dataList) where U : IDataList<T>
    {
        foreach (var lootTable in dataListSource.childTables)
        {
            lootTable.dataListGenerator.FillDataList(ref dataList);
        }

        foreach (var itemSlot in dataListSource.dataItems)
        {
            dataList.AddItems(itemSlot.item, itemSlot.count);
        }
    }
}

