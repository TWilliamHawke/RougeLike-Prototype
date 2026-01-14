using System.Collections.Generic;

public interface IDataListTable<T>
{
    bool getOnlyOneElenemt { get; }
    IEnumerable<IDataListElementSource<T>> GetDataListSources();
}
