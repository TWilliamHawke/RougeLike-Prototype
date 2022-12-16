using UnityEngine;
using Map.Objects;

namespace Map.UI
{
    public class TaskPanelController : MonoBehaviour
    {
        [SerializeField] TaskPanel _taskPanel;

        [InjectField] MapObjectObserver _mapObjectObserver;

        IMapObject _currentMapObject;

        //used in editor
        public void FinalizeInjection()
        {
            _mapObjectObserver.OnMapObjectChange += ChangeTargetMapObject;
            _currentMapObject = _mapObjectObserver.currentMapObject;
            UpdateTaskPanel();
        }

        public void ChangeTargetMapObject(IMapObject mapObject)
        {
            if (_currentMapObject == mapObject) return;
            _currentMapObject = mapObject;
            UpdateTaskPanel();
        }

        //used in editor
        public void UpdateTaskPanel()
        {
            _taskPanel.SetTask(_currentMapObject.currentTask);
        }
    }
}

