using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour, IInjectionTarget
    {
        [InjectField] Player _player;

        [Header("Injectors")]
        [SerializeField] Injector _playerInjector;
        [SerializeField] Injector _mainCameraInjector;

        public bool waitForAllDependencies => false;

        private void Awake()
        {
            _playerInjector.AddInjectionTarget(this);
            _mainCameraInjector.SetDependency(GetComponent<Camera>());
        }

        void LateUpdate()
        {
            if(_player is null) return;
            transform.position = transform.position.ReplaceXYFrom(_player.transform.position);
        }

        public void CenterAt(Vector3 pos)
        {
            transform.position = transform.position.ReplaceXYFrom(pos);
        }

        public void CenterAt(Vector2 pos)
        {
            transform.position = transform.position.ReplaceXYFrom(pos);
        }

        public void FinalizeInjection()
        {
            
        }
    }
}