using UnityEngine;

namespace Map
{
    [CreateAssetMenu(fileName = "TaskTemplate", menuName = "Map/TaskTemplate")]
    public class KillEnemiesTaskTemplate : TaskTemplate, IDynamicTaskData
    {
        [SerializeField] CustomEvent _onLocalTaskChange;
        [SerializeField] LocalString _taskText = "Kill {0} enemies";

        public override ITaskController CreateTask(ItaskData iconData, string taskText)
        {
            return new KillEnemiesTask(iconData, this);
        }

        public string CreateTaskText(int targetCount)
        {
            return _taskText.GetLocalText(new TextReplacer
            {
                pattern = "{0}",
                replacer = targetCount.ToString(),
            });
        }

        public void TriggerTaskChangeEvent()
        {
            _onLocalTaskChange?.Invoke();
        }
    }
}


