using UnityEngine;
using Map.Zones;

namespace Map.UI
{
    public class TaskPanelController : MonoBehaviour
    {
        [SerializeField] TaskPanel _taskPanel;

        [InjectField] MapZonesObserver _mapZonesObserver;

        IMapZone _currentMapZone;

        //used in editor
        public void FinalizeInjection()
        {
            _mapZonesObserver.OnMapZoneChange += ChangeTargetMapZone;
            _currentMapZone = _mapZonesObserver.currentMapZone;
            UpdateTaskPanel();
        }

        public void ChangeTargetMapZone(IMapZone mapZone)
        {
            if (_currentMapZone == mapZone) return;
            _currentMapZone = mapZone;
            UpdateTaskPanel();
        }

        //used in editor
        public void UpdateTaskPanel()
        {
            _taskPanel.SetTask(_currentMapZone.currentTask);
        }
    }
}

