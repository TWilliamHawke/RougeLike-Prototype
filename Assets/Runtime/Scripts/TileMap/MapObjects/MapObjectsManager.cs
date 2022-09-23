using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using Map.Objects.UI;
using Map.Locations;

namespace Map.Objects
{
	public class MapObjectsManager : MonoBehaviour, IMapObjectsCreator, IDependency
	{
	    [SerializeField] Site _sitePrefab;
		[SerializeField] Tilemap _tileMap;
		[SerializeField] Injector _objectsManagerInjector;
		[SerializeField] Injector _topLocationPanelInjector;
		[SerializeField] Injector _actionsScreenInjector;

		MapObjectsUIController _uIController;

        public bool isReadyForUse => _uIController != null;

        public event UnityAction OnReadyForUse;

		private void Awake()
		{
			_objectsManagerInjector.AddDependency(this);
		}

		public void SetLocation(Location location)
		{
			_uIController = new MapObjectsUIController(location);
			_topLocationPanelInjector.AddInjectionTarget(_uIController);
			_actionsScreenInjector.AddInjectionTarget(_uIController);
			OnReadyForUse?.Invoke();
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

