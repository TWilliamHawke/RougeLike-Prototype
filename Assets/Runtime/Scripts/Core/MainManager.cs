using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using Entities;
using Core.Input;
using Entities.Player;

namespace Core
{
    public class MainManager : MonoBehaviour
	{

		[SerializeField] TileMapManager _tileMapManager;
		[SerializeField] GameObjectsManager _gameObjectManager;
		[SerializeField] InputManager _inputManager;
		[SerializeField] UIManager _UIManager;
		[SerializeField] EntitiesManager _entitiesManager;
		[SerializeField] PlayerDataManager _playerDataManager;

	    void Awake()
	    {
			_gameObjectManager.StartUp();
			
			//other objects will subscribe on input events
			_inputManager.StartUp();

			//_healthbarCanvas should subscribe on events before player spawn
			_UIManager.StartUp();
			
	        _tileMapManager.StartUp();
			_entitiesManager.StartUp();
			_playerDataManager.StartUp();
	    }
	

	}
}