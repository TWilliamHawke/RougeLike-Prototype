using System.Collections;

public interface IDataList<T> : IEnumerable
{
    void AddItems(T item, int count);
}

