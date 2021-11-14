using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

namespace Core
{
    public class CoreManager : MonoBehaviour
	{

		[SerializeField] TileMapManager _tileMapManager;
		[SerializeField] GameObjectsManager _gameObjectManager;

	    void Awake()
	    {
			_gameObjectManager.SetSharedGameObjects();
	        _tileMapManager.Init();
	    }
	

	    void Update()
	    {

	    }
	}
}