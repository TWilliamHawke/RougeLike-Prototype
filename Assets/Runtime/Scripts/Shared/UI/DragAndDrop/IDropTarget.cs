using UnityEngine.UI;

namespace UI.DragAndDrop
{
    public interface IDropTarget<in T> : IDropTarget
    {
        void DropData(T data);
        bool DataIsMeet(T data);
    }

    public interface IDropTarget
    {
        bool checkImageAlpha { get; }
        void Highlight()
        {
        }
        void UnHighlight()
        {
        }
    }
}