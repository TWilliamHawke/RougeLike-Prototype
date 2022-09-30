using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core
{
	public class GameObjectsManager : MonoBehaviour
	{
	    [SerializeField] CameraController _mainCamera;
		[SerializeField] Player _player;
		[SerializeField] GameObjects _gameObjects;
		[SerializeField] Tilemap _tilemap;

		public void StartUp()
		{
			_mainCamera.SetPlayer(_player);
			SetSharedGameObjects();
		}

		void SetSharedGameObjects()
		{
			_gameObjects.player = _player;
			_gameObjects.mainCamera = _mainCamera.GetComponent<Camera>();
			_gameObjects.cameraController = _mainCamera;
			_gameObjects.tilemap = _tilemap;
		}

	}
}