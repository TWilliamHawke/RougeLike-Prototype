using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using Entities;
using Core.Input;

namespace Core
{
    public class MainManager : MonoBehaviour
	{

		[SerializeField] TileMapManager _tileMapManager;
		[SerializeField] GameObjectsManager _gameObjectManager;
		[SerializeField] InputManager _inputManager;
		[SerializeField] UIManager _UIManager;
		[SerializeField] EntitiesManager _entitiesManager;

	    void Awake()
	    {
			_gameObjectManager.StartUp();

			//_healthbarCanvas should subscribe on events before player spawn
			_UIManager.StartUp();
			
	        _tileMapManager.StartUp();
			_inputManager.StartUp();
			_entitiesManager.StartUp();
	    }
	

	}
}