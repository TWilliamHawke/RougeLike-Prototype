using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

namespace Core
{
    public class MainManager : MonoBehaviour
	{

		[SerializeField] TileMapManager _tileMapManager;
		[SerializeField] GameObjectsManager _gameObjectManager;
		[SerializeField] InputManager _inputManager;
		[SerializeField] UIManager _UIManager;

	    void Awake()
	    {
			_gameObjectManager.SetSharedGameObjects();
	        _tileMapManager.Init();
			_inputManager.StartUp();
			_UIManager.StartUp();
	    }
	

	}
}