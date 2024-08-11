using UnityEngine;

namespace UI.DragAndDrop
{
    public interface IDragController
    {
        bool TryFindDropTarget(out IDropTarget target, Vector2 raycastPos);
        DragableUIElement CreateElement();
        void DropData();
    }
}