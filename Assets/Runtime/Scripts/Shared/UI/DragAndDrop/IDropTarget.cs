namespace UI.DragAndDrop
{
    public interface IDropTarget<T>
    {
        void SetDragableData(T data);
        bool DataIsMeet(T data);
    }
}