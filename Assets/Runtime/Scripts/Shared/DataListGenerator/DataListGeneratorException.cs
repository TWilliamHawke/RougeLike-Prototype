using Items;

public class DataListGeneratorException<T> : System.Exception
{
    T _buggedLootTable;

    public T lootTable => _buggedLootTable;

    public DataListGeneratorException(T lootTable)
    {
        _buggedLootTable = lootTable;
    }
}

