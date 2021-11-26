using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core
{
	[CreateAssetMenu(fileName ="GameObjects", menuName ="map/GeneratorConfig")]
	public class GameObjects : ScriptableObject
	{
	    public CameraController mainCamera { get; set; }
		public PlayerCore player { get; set; }
		public Tilemap tilemap { get; set; }
	}
}