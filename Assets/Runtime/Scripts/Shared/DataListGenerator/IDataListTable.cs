public interface IDataListTable<T>
{
    DataListGenerator<T> dataListGenerator { get; }
    IDataListTable<T>[] childTables { get; }
    IDataListElement<T>[] dataItems { get; }
    bool getOnlyOneElenemt { get; }
    float chanceOfNone { get; }
}

