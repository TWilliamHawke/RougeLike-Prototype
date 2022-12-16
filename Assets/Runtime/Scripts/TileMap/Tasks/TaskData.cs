using UnityEngine;

namespace Map
{
	public struct TaskData
	{
	    public string taskText { get; init; }
        public Sprite icon { get; init; }
        public string displayName { get; init; }
        public bool isDone { get; init; }

        public TaskData(string taskText, string displayName, Sprite icon, bool isDone)
        {
            this.taskText = taskText;
            this.displayName = displayName;
            this.icon = icon;
            this.isDone = isDone;
        }
    }
}

