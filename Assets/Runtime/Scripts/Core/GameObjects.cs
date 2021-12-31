using System.Collections;
using System.Collections.Generic;
using Core.Input;
using Entities.Player;
using Map;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Core
{
    public class GameObjects : ScriptableObject
    {
        [SerializeField] TilemapController _tilemapController;
        [SerializeField] InputController _inputController;

        public Camera mainCamera { get; set; }
        public CameraController cameraController { get; set; }
        public PlayerCore player { get; set; }
        public Tilemap tilemap { get; set; }

        public TilemapController tilemapController => _tilemapController;
        public InputController inputController => _inputController;
        public IAudioSource mainAudioSource => player.body;


    }
}