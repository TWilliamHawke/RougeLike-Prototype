using System.Collections;
using System.Collections.Generic;
using Map.Zones;
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
		void CreateMapZones(MapZonesManager mapZonesManager);
	}
}