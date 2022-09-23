using System.Collections;
using System.Collections.Generic;
using Map.Locations;
using UnityEngine;
using System.Linq;

namespace Map.Objects.UI
{
    public class MapObjectsUIController : IInjectionTarget
    {
        HashSet<MapObject> _visitedMapObjects = new HashSet<MapObject>();
        Location _currentLocation;
        MapObject _currentMapObject;

        [InjectField] TopLocationInfoPanel _topInfoPanel;
        [InjectField] ActionsScreen _actionsScreen;

        public bool waitForAllDependencies => true;

        public MapObjectsUIController(Location currentLocation)
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
            _topInfoPanel.SetLocationIcon(_currentLocation.icon);
            _topInfoPanel.SetLocationName(_currentLocation.name);
            _topInfoPanel.SetTask(_currentLocation.task);
        }

        private void EnterToLocation(MapObject mapObject)
        {
            _visitedMapObjects.Add(mapObject);
            SetCurrentMapObject(mapObject);
        }

        private void SetCurrentMapObject(MapObject mapObject)
        {
            mapObject.OnTaskChange += UpdateTaskText;
            _currentMapObject = mapObject;
            _topInfoPanel.SetLocationIcon(mapObject.template.icon);
            _topInfoPanel.SetLocationName(mapObject.template.displayName);
            _topInfoPanel.SetTask(mapObject.task);
        }

        private void ExitFromLocation(MapObject mapObject)
        {
            mapObject.OnTaskChange -= UpdateTaskText;
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

        private void UpdateTaskText(MapObjectTask task)
        {
            _topInfoPanel.SetTask(task);
        }

        private void OpenActionPanel()
        {
            if (!_currentMapObject) return;
            _actionsScreen.SetTitle(_currentMapObject.template.displayName);
            _actionsScreen.SetIcon(_currentMapObject.template.icon);
            _actionsScreen.Open();
        }
    }
}

