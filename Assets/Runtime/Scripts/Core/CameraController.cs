using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        PlayerCore _player;

        void LateUpdate()
        {
			transform.position = transform.position.ChangeXYFrom(_player.transform.position);
        }

        public void SetPlayer(PlayerCore player)
        {
            _player = player;
        }

        public void CenterAt(Vector3 pos)
        {
            transform.position = transform.position.ChangeXYFrom(pos);
        }

        public void CenterAt(Vector2 pos)
        {
            transform.position = transform.position.ChangeXYFrom(pos);
        }
    }
}