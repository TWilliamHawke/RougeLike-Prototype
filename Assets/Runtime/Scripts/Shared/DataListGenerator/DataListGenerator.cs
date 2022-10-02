using System.Collections.Generic;
using UnityEngine;

public class DataListGenerator<T>
{
    IDataListSource<T> dataListSource;

    public DataListGenerator(IDataListSource<T> dataListSource)
    {
        this.dataListSource = dataListSource;
    }

    public void FillDataList<U>(ref U dataList) where U : IDataList<T>
    {
        if (Random.Range(0f, 1f) < dataListSource.chanceOfNone) return;

        if (dataListSource.getOnlyOneElenemt)
        {
            GetRandomItems(ref dataList);
        }
        else
        {
            GetAllItems(ref dataList);
        }
    }

    public void CheckErrors()
    {
        var set = new HashSet<DataListGenerator<T>>();
        CheckTableInSet(ref set);
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

    private void GetRandomItems<U>(ref U dataList) where U : IDataList<T>
    {
        int variantsCount = dataListSource.childTables.Length + dataListSource.dataItems.Length;
        if (variantsCount == 0) return;

        int index = Random.Range(0, dataListSource.childTables.Length + dataListSource.dataItems.Length);

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

    private void GetAllItems<U>(ref U dataList) where U : IDataList<T>
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

