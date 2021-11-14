using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace PlayerScripts
{
	public class Player : MonoBehaviour, ICanMove
	{
		[SerializeField] GameObjects _gameObjects;

        public void MoveTo(Vector3 position)
        {
            TeleportTo(position);


        }

        public void TeleportTo(Vector3 position)
        {
            transform.position = transform.position.ChangeXYFrom(position);
			_gameObjects.mainCamera.CenterAt(position);
        }

	}
}