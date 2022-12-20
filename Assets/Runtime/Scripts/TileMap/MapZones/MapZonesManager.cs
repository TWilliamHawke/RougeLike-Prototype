using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Zones
{
	public class MapZonesManager : MonoBehaviour, IMapZonesCreator
	{
	    [SerializeField] Site _sitePrefab;
	    [SerializeField] Encounter _encounterPrefab;
		[SerializeField] Tilemap _tileMap;
		[SerializeField] TileMapManager tileMapManager;
		[Header("Inectors")]
		[SerializeField] Injector _thisInjector;
		[SerializeField] Injector _mapObserverInjector;

		MapZonesObserver _mapZonesObserver;

		private void Awake()
		{
			_mapZonesObserver = new MapZonesObserver(tileMapManager.location);
			_mapObserverInjector.SetDependency(_mapZonesObserver);
			_thisInjector.SetDependency(this);
		}

        public Site CreateSite(Vector3 position)
        {
            var site = _tileMap.CreateChild(_sitePrefab, position);
			_mapZonesObserver.AddToObserve(site);
			return site;
        }

		public Encounter CreateEncounter(Vector3 position)
		{
			var encounter = _tileMap.CreateChild(_encounterPrefab, position);
			_mapZonesObserver.AddToObserve(encounter);
			return encounter;
		}
    }

	public interface IMapZonesCreator
	{
		Site CreateSite(Vector3 position);
	}
}

