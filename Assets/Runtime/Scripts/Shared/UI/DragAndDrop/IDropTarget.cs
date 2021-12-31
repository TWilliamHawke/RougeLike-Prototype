namespace UI.DragAndDrop
{
    public interface IDropTarget<in T>
    {
        void DropData(T data);
        bool DataIsMeet(T data);
    }
}