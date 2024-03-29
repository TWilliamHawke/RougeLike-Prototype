using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Map
{
    public class KillEnemiesTask : ITaskController, IObserver<Entity>
    {
        public TaskData currentTask { get; private set;}
        public HashSet<Entity> enemiesFromLocation { get; private set; } = new();

        IIconData _locationTemplate;
        CustomEvent _onLocalTaskChange;

        public KillEnemiesTask(IIconData locationTemplate, CustomEvent onLocalTaskChange)
        {
            _locationTemplate = locationTemplate;
            _onLocalTaskChange = onLocalTaskChange;
            CreateLootTask();
        }

        public void AddToObserve(Entity entity)
        {
            enemiesFromLocation.Add(entity);
            entity.OnDeath += RemoveFromObserve;
            CreateKillTask();
        }

        public void RemoveFromObserve(Entity enemy)
        {
            if (!enemiesFromLocation.Contains(enemy)) return;
            enemiesFromLocation.Remove(enemy);

            if (enemiesFromLocation.Count > 0)
            {
                CreateKillTask();
            }
            else
            {
                CreateLootTask();
            }

            _onLocalTaskChange?.Invoke();
        }

        private void CreateKillTask()
        {
            currentTask = new TaskData
            {
                displayName = _locationTemplate.displayName,
                icon = _locationTemplate.icon,
                //HACK always is Kill all wolves
                taskText = $"Kill all wolves ({enemiesFromLocation.Count} remains)",
                isDone = false,
            };
        }

        private void CreateLootTask()
        {
                currentTask = new TaskData
                {
                    displayName = _locationTemplate.displayName,
                    icon = _locationTemplate.icon,
                    taskText = "Click to loot",
                    isDone = true,
                };
        }
    }
}


