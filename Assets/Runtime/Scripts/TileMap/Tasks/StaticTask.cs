using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class StaticTask : ITaskController
    {
        public TaskData currentTask { get; init; }

        public StaticTask(TaskData currentTask)
        {
            this.currentTask = currentTask;
        }
    }
}


