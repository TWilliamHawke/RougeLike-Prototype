namespace UI.DragAndDrop
{
    public interface IDragDataSource<T>
    {
        T dragData { get; }
        DragableUIElement<T> dragableElementPrefab { get; }
    }
}
