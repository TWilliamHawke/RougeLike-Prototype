using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.DragAndDrop
{
    public interface IDragDataSource<T> : IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        T dragData { get; }
        DragableUIElement<T> dragableElementPrefab { get; }
        Canvas dragCanvas { get; }
    }
}
