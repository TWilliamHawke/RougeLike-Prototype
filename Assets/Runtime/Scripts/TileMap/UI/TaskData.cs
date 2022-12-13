using UnityEngine;

namespace Map
{
	public struct TaskData
	{
	    public string taskText { get; init; }
		public bool objectIsLocked { get; init; }
        public Sprite icon { get; init; }
        public string displayName { get; init; }

        public TaskData(string taskText, bool objectIsLocked, string displayName, Sprite icon)
        {
            this.taskText = taskText;
            this.objectIsLocked = objectIsLocked;
            this.displayName = displayName;
            this.icon = icon;
        }
    }
}

