using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using Map.Objects.UI;
using Map.Locations;

namespace Map.Objects
{
	public class MapObjectsManager : MonoBehaviour, IMapObjectsCreator
	{
	    [SerializeField] Site _sitePrefab;
		[SerializeField] Tilemap _tileMap;
		[SerializeField] Injector _objectsManagerInjector;
		[SerializeField] Injector _topLocationPanelInjector;
		[SerializeField] Injector _actionsScreenInjector;

		[SerializeField] TileMapManager tileMapManager;

		MapObjectsUIController _uIController;

		private void Awake()
		{
			_uIController = new MapObjectsUIController(tileMapManager.location);
			_topLocationPanelInjector.AddInjectionTarget(_uIController);
			_actionsScreenInjector.AddInjectionTarget(_uIController);
			_objectsManagerInjector.AddDependency(this);
		}

        public Site CreateSite(Vector3 position)
        {
            var site = _tileMap.CreateChild(_sitePrefab, position);
			_uIController.AddToObserve(site);
			return site;
        }
    }

	public interface IMapObjectsCreator
	{
		Site CreateSite(Vector3 position);
	}
}

