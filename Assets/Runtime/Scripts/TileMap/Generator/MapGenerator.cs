using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Map.Generator
{
	public abstract class MapGenerator : ScriptableObject
	{
		public abstract LocationMapData StartGeneration(Tilemap tilemap);
	}

	interface IGenerationAlgorithm
	{
		int[,] Create2dArray();
		int[] walkableTiles { get; }
		
	}
}