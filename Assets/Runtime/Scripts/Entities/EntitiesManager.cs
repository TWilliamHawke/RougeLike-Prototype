using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.PlayerScripts;

namespace Entities
{
	public class EntitiesManager : MonoBehaviour
	{
	    [SerializeField] Player _player;

		public void StartUp()
		{
			_player.Init();
		}
	}
}