public interface IDataListSource<T>
{
    DataListGenerator<T> dataListGenerator { get; }
    IDataListSource<T>[] childTables { get; }
    IDataCount<T>[] dataItems { get; }
    bool getOnlyOneElenemt { get; }
    float chanceOfNone { get; }
}

