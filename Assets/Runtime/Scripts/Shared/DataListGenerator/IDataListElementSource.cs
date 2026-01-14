using System.Collections.Generic;

public interface IDataListElementSource<T>
{
    int weight { get; }
    IEnumerable<IDataListElement<T>> GetElements();
}