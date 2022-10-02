public class DataListGeneratorException<T> : System.Exception
{
    DataListGenerator<T> _buggedLootTable;

    public DataListGenerator<T> lootTable => _buggedLootTable;

    public DataListGeneratorException(DataListGenerator<T> lootTable)
    {
        _buggedLootTable = lootTable;
    }
}

