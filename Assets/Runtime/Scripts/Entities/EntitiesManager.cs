using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.PlayerScripts;

namespace Entities
{
	public class EntitiesManager : MonoBehaviour
	{
	    [SerializeField] Player _player;
		[SerializeField] Enemy _testEnemy;

		public void StartUp()
		{
			_player.Init();
			_testEnemy.Init();
		}
	}
}