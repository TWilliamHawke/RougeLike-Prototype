using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core
{
	[CreateAssetMenu(fileName ="GameObjects", menuName ="map/GeneratorConfig")]
	public class GameObjects : ScriptableObject
	{
	    public CameraController mainCamera { get; set; }
		public Player player { get; set; }
		public Tilemap tilemap { get; set; }
	}
}