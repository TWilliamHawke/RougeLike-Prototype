using System.Collections;
using System.Collections.Generic;
using Map.Locations;
using UnityEngine;
using System.Linq;
using Map.Objects;

namespace Map.UI
{
    public class TaskPanelController : IInjectionTarget
    {
        HashSet<IMapObject> _visitedMapObjects = new HashSet<IMapObject>();
        Location _currentLocation;
        IMapObject _currentMapObject;

        [InjectField] TaskPanel _topInfoPanel;
        [InjectField] ActionsScreen _actionsScreen;

        public bool waitForAllDependencies => true;

        public TaskPanelController(Location currentLocation)
        {
            _currentLocation = currentLocation;
        }

        public void AddToObserve(MapObject mapObject)
        {
            mapObject.OnPlayerEnter += EnterToLocation;
            mapObject.OnPlayerExit += ExitFromLocation;
        }

        void IInjectionTarget.FinalizeInjection()
        {
            SetCurrentLocationData();
            _topInfoPanel.OnClick += OpenActionPanel;
            //_topInfoPanel.SetActive();
        }

        private void SetCurrentLocationData()
        {
            _topInfoPanel.SetTask(_currentLocation.task);
        }

        private void EnterToLocation(IMapObject mapObject)
        {
            _visitedMapObjects.Add(mapObject);
            SetCurrentMapObject(mapObject);
        }

        private void SetCurrentMapObject(IMapObject mapObject)
        {
            mapObject.behavior.OnTaskChange += UpdateTaskText;
            mapObject.behavior.actionsController.OnActionStateChange += SetActions;
            _currentMapObject = mapObject;
            _topInfoPanel.SetTask(mapObject.behavior.task);
        }

        private void ExitFromLocation(IMapObject mapObject)
        {
            mapObject.behavior.OnTaskChange -= UpdateTaskText;
            mapObject.behavior.actionsController.OnActionStateChange -= SetActions;

            if (_visitedMapObjects.Contains(mapObject))
            {
                _visitedMapObjects.Remove(mapObject);
            }

            if (_visitedMapObjects.Count > 0)
            {
                SetCurrentMapObject(_visitedMapObjects.First());
            }
            else
            {
                SetCurrentLocationData();
            }
        }

        private void UpdateTaskText(TaskData task)
        {
            _topInfoPanel.SetTask(task);
        }

        private void OpenActionPanel()
        {
            if (_currentMapObject is null) return;
            _actionsScreen.SetTitle(_currentMapObject.template.displayName);
            _actionsScreen.SetIcon(_currentMapObject.template.icon);
            SetActions();
            _actionsScreen.Open();
        }

        private void SetActions()
        {
            _actionsScreen.SetActions(_currentMapObject.behavior.actionsController);
        }
    }
}

