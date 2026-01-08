using System.Collections;
using System.Collections.Generic;
using Map.Zones;
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

        public void HandleSpawnQueue(ISpawnQueue spawnQueue)
        {
            //do nothing
        }
    }
}


