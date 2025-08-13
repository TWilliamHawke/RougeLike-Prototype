using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

namespace Core.Input
{
    public class ClickUnwalkableTile : IClickAction
    {
        public void ProcessClick(ITileClickData tile)
        {
			//do nothing
        }

        public bool CanBeUsedOnTile(ITileClickData tile)
        {
        	return !tile.isWalkableAndEmpty;
        }

    }
}