using System.Collections;
using System.Collections.Generic;
using Map.Objects;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Map.Generator
{
	public abstract class GeneratorConfig : ScriptableObject
	{
		public abstract IGenerationLogic GetLogic(Tilemap tilemap);
	}

	public interface IGenerationLogic
	{
		LocationMapData StartGeneration();
		void CreateMapObjects(MapObjectsManager mapObjectsManager);
	}
}