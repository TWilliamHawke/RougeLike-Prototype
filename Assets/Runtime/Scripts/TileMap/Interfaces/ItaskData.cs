using UnityEngine;

namespace Map
{
    public interface ItaskData
    {
        public string taskText { get; }
        public Sprite icon { get; }
        public string displayName { get; }
    }
}

