using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.DragAndDrop
{
    public interface IDragDataSource<T> : IDragDataSource
    {
        T dragData { get; }
    }

    public interface IDragDataSource
    {
        IDragController dataHandler { get; }
        DragableUIElement CreateElement()
        {
            return dataHandler.CreateElement();
        }
        bool TryFindDropTarget(out IDropTarget target, Vector2 raycastPos)
        {
            return dataHandler.TryFindDropTarget(out target, raycastPos);
        }
        void DropData()
        {
            dataHandler.DropData();
        }
    }
}
