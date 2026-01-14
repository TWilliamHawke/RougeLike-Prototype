public struct DataListElement<T> : IDataListElement<T>
{
    public T element { get; init; }
    public int count { get; init; }
}