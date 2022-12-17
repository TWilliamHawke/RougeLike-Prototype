using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Objects
{
	public class MapObjectsManager : MonoBehaviour, IMapObjectsCreator
	{
	    [SerializeField] Site _sitePrefab;
	    [SerializeField] Encounter _encounterPrefab;
		[SerializeField] Tilemap _tileMap;
		[SerializeField] TileMapManager tileMapManager;
		[Header("Inectors")]
		[SerializeField] Injector _thisInjector;
		[SerializeField] Injector _mapObserverInjector;

		MapObjectObserver _mapObjectObserver;

		private void Awake()
		{
			_mapObjectObserver = new MapObjectObserver(tileMapManager.location);
			_mapObserverInjector.SetDependency(_mapObjectObserver);
			_thisInjector.SetDependency(this);
		}

        public Site CreateSite(Vector3 position)
        {
            var site = _tileMap.CreateChild(_sitePrefab, position);
			_mapObjectObserver.AddToObserve(site);
			return site;
        }

		public Encounter CreateEncounter(Vector3 position)
		{
			var encounter = _tileMap.CreateChild(_encounterPrefab, position);
			_mapObjectObserver.AddToObserve(encounter);
			return encounter;
		}
    }

	public interface IMapObjectsCreator
	{
		Site CreateSite(Vector3 position);
	}
}

