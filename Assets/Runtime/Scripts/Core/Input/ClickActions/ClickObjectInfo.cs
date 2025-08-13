using System.Collections;
using System.Collections.Generic;
using Core.Input;
using Map;
using UnityEngine;

namespace Core.Input
{
    public class ClickObjectInfo : IClickAction
    {
        public bool CanBeUsedOnTile(ITileClickData tile)
        {
            return true;
        }

        public void ProcessClick(ITileClickData tile)
        {
            foreach (var obj in tile.entitiesOnTile)
            {
                Debug.Log(obj);
                return;
            }
        }

    }
}
