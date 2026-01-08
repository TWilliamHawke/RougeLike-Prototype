using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Map
{
    public class KillEnemiesTask : ITaskController, IObserver<Entity>
    {
        public TaskData currentTask { get; private set;}

        ItaskData _locationTemplate;
        IDynamicTaskData _taskTemplate;
        HashSet<Entity> _enemiesFromLocation { get; init; } = new();

        public KillEnemiesTask(ItaskData locationTemplate, IDynamicTaskData taskTemplate)
        {
            _locationTemplate = locationTemplate;
            _taskTemplate = taskTemplate;
            currentTask = CreateLootTask();
        }

        public void AddToObserve(Entity entity)
        {
            //TODO add faction check
            _enemiesFromLocation.Add(entity);
            entity.OnDeath += RemoveFromObserve;
            currentTask = CreateKillTask();
        }

        public void HandleSpawnQueue(ISpawnQueue spawnQueue)
        {
            spawnQueue.AddObserver(this);
        }

        public void RemoveFromObserve(Entity enemy)
        {
            if (!_enemiesFromLocation.Contains(enemy)) return;
            _enemiesFromLocation.Remove(enemy);

            if (_enemiesFromLocation.Count > 0)
            {
                currentTask = CreateKillTask();
            }
            else
            {
                currentTask = CreateLootTask();
            }

            _taskTemplate.TriggerTaskChangeEvent();
        }

        private TaskData CreateKillTask()
        {
            return new TaskData
            {
                displayName = _locationTemplate.displayName,
                icon = _locationTemplate.icon,
                taskText = _taskTemplate.CreateTaskText(_enemiesFromLocation.Count),
                isDone = false,
            };
        }

        private TaskData CreateLootTask()
        {
            return new TaskData
            {
                displayName = _locationTemplate.displayName,
                icon = _locationTemplate.icon,
                taskText = _locationTemplate.taskText,
                isDone = true,
            };
        }
    }
}


