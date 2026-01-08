using UnityEngine;

namespace Map
{
    public class TaskTemplate : ScriptableObject
    {
        public virtual ITaskController CreateTask(ItaskData iconData, string taskText)
        {
            TaskData taskData = new()
            {
                displayName = iconData.displayName,
                icon = iconData.icon,
                taskText = taskText,
                isDone = true,
            };

            return new StaticTask(taskData);
        }
    }
}


