using System.Collections;
using System.Collections.Generic;
using Map.Generator;
using Map.Objects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Locations
{
    [CreateAssetMenu(fileName = "GeneratedLocation", menuName = "Map/Location")]
    public class GeneratedLocation : Location
    {
        [SerializeField] MapGenerator _generator;

        public override TaskData currentTask { get; protected set; }

        public override LocationMapData Create(Tilemap tilemap)
        {
            currentTask = new TaskData
            {
                displayName = displayName,
                taskText = "Explore the location",
                icon = icon,
                isDone = true,
            };
            
            return _generator.StartGeneration(tilemap);
        }
    }
}

