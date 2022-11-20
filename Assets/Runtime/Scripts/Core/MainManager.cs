using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using Entities;
using Core.Input;
using Entities.PlayerScripts;

namespace Core
{
    public class MainManager : MonoBehaviour
    {

        [SerializeField] TileMapManager _tileMapManager;
        [SerializeField] InputManager _inputManager;
        [SerializeField] EntitiesManager _entitiesManager;
        [SerializeField] PlayerDataManager _playerDataManager;

        void Awake()
        {
            //inventory should init before ui
            _playerDataManager.StartUp();

            _tileMapManager.StartUp();
            _entitiesManager.StartUp();
        }


    }
}