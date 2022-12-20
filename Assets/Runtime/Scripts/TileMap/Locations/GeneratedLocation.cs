using System.Collections;
using System.Collections.Generic;
using Map.Generator;
using Map.Zones;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Locations
{
    [CreateAssetMenu(fileName = "GeneratedLocation", menuName = "Map/Location")]
    public class GeneratedLocation : Location, IInjectionTarget
    {
        [SerializeField] GeneratorConfig _generator;
        [SerializeField] Injector _zonesManagerInjector;

        [InjectField] MapZonesManager _mapZonesManager;

        public override TaskData currentTask { get; protected set; }

        public bool waitForAllDependencies => false;

        IGenerationLogic _generatorLogic;

        public override LocationMapData Create(Tilemap tilemap)
        {
            tilemap.ClearAllTiles();
            tilemap.DestroyChildren();

            currentTask = new TaskData
            {
                displayName = displayName,
                taskText = "Explore the location",
                icon = icon,
                isDone = true,
            };

            _generatorLogic = _generator.GetLogic(tilemap);

            var rawMapData = _generatorLogic.StartGeneration();
            _zonesManagerInjector.AddInjectionTarget(this);

            return rawMapData;
        }

        public void FinalizeInjection()
        {
            _generatorLogic.CreateMapZones(_mapZonesManager);
        }
    }
}

