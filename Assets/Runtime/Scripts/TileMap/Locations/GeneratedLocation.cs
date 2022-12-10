using System.Collections;
using System.Collections.Generic;
using Map.Generator;
using Map.Objects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Locations
{
	[CreateAssetMenu(fileName="GeneratedLocation", menuName ="Map/Location")]
    public class GeneratedLocation : Location
    {
		[SerializeField] MapGenerator _generator;

        public override MapObjectTaskData task => _task;

        MapObjectTaskData _task = new MapObjectTaskData("Explore the location", true);

        public override LocationMapData Create(Tilemap tilemap)
        {
            return _generator.StartGeneration(tilemap);
        }
    }
}

