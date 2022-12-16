using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Map.Locations;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    public class MapObjectObserver
    {
        HashSet<IMapObject> _visitedMapObjects = new HashSet<IMapObject>();
        Location _currentLocation;
        public IMapObject currentMapObject { get; private set; }

        public event UnityAction<IMapObject> OnMapObjectChange;

        public MapObjectObserver(Location currentLocation)
        {
            _currentLocation = currentLocation;
			currentMapObject = currentLocation;
        }

        public void AddToObserve(MapObject mapObject)
        {
            mapObject.OnPlayerEnter += EnterToLocation;
            mapObject.OnPlayerExit += ExitFromLocation;
        }

        private void EnterToLocation(IMapObject mapObject)
        {
            _visitedMapObjects.Add(mapObject);
            OnMapObjectChange?.Invoke(mapObject);
        }

        private void ExitFromLocation(IMapObject mapObject)
        {
            if (_visitedMapObjects.Contains(mapObject))
            {
                _visitedMapObjects.Remove(mapObject);
            }

            if (_visitedMapObjects.Count > 0)
            {
                OnMapObjectChange?.Invoke(_visitedMapObjects.First());
            }
            else
            {
				OnMapObjectChange?.Invoke(_currentLocation);
            }
        }


    }
}


