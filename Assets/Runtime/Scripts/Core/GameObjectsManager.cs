using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;
using UnityEngine.Tilemaps;
using Items;

namespace Core
{
	public class GameObjectsManager : MonoBehaviour
	{
	    [SerializeField] CameraController _mainCamera;
		[SerializeField] PlayerCore _player;
		[SerializeField] GameObjects _gameObjects;
		[SerializeField] Tilemap _tilemap;
		[SerializeField] Inventory _inventory;

		public void StartUp()
		{
			_mainCamera.SetPlayer(_player);
			SetSharedGameObjects();
			_inventory.Init();
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