using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Map.Locations;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Zones
{
    public class MapZonesObserver: IObserver<MapZone>
    {
        HashSet<IMapZone> _visitedMapZones = new HashSet<IMapZone>();
        Location _currentLocation;
        public IMapZone currentMapZone { get; private set; }

        public event UnityAction<IMapZone> OnMapZoneChange;

        public MapZonesObserver(Location currentLocation)
        {
            _currentLocation = currentLocation;
			currentMapZone = currentLocation;
        }

        public void AddToObserve(MapZone mapZone)
        {
            mapZone.OnPlayerEnter += EnterToZone;
            mapZone.OnPlayerExit += ExitFromZone;
        }

        public void RemoveFromObserve(MapZone mapZone)
        {
            mapZone.OnPlayerEnter -= EnterToZone;
            mapZone.OnPlayerExit -= ExitFromZone;
            _visitedMapZones.Remove(mapZone);

            if(currentMapZone != (IMapZone)mapZone) return;
            ExitFromZone(mapZone);
        }

        private void EnterToZone(IMapZone mapZone)
        {
            _visitedMapZones.Add(mapZone);
            OnMapZoneChange?.Invoke(mapZone);
        }

        private void ExitFromZone(IMapZone mapZone)
        {
            if (_visitedMapZones.Contains(mapZone))
            {
                _visitedMapZones.Remove(mapZone);
            }

            if (_visitedMapZones.Count > 0)
            {
                currentMapZone = _visitedMapZones.First();
            }
            else
            {
                currentMapZone = _currentLocation;
            }
			
            OnMapZoneChange?.Invoke(currentMapZone);
        }


    }
}


