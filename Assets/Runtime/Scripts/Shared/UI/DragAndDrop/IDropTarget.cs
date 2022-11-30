using UnityEngine.UI;

namespace UI.DragAndDrop
{
    public interface IDropTarget<in T>
    {
        bool checkImageAlpha { get; }
        void DropData(T data);
        bool DataIsMeet(T data);
        void Highlight()
        {
        }
        void UnHighlight()
        {
        }
    }
}