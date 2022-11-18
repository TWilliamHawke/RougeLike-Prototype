using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        Player _player;

        void LateUpdate()
        {
			transform.position = transform.position.ReplaceXYFrom(_player.transform.position);
        }

        public void SetPlayer(Player player)
        {
            _player = player;
        }

        public void CenterAt(Vector3 pos)
        {
            transform.position = transform.position.ReplaceXYFrom(pos);
        }

        public void CenterAt(Vector2 pos)
        {
            transform.position = transform.position.ReplaceXYFrom(pos);
        }
    }
}