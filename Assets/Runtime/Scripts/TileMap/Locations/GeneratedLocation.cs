using System.Collections;
using System.Collections.Generic;
using Map.Generator;
using Map.Objects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Locations
{
    [CreateAssetMenu(fileName = "GeneratedLocation", menuName = "Map/Location")]
    public class GeneratedLocation : Location, IInjectionTarget
    {
        [SerializeField] GeneratorConfig _generator;
        [SerializeField] Injector _objectsManagerInjector;

        [InjectField] MapObjectsManager _mapObjectsManager;

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
            _objectsManagerInjector.AddInjectionTarget(this);

            return rawMapData;
        }

        public void FinalizeInjection()
        {
            _generatorLogic.CreateMapObjects(_mapObjectsManager);
        }
    }
}

