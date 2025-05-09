using System.Collections.Generic;
using Map;

namespace Entities.Behavior
{
    public class VisibilityChecker
    {
        public bool IsVisible(TileNode from, TileNode to)
        {
            var nodes = GetTilesBetween(from, to);

            foreach (var node in nodes)
            {
                if (!node.isWalkable) return false;
            }
            return true;
        }

        List<TileNode> GetTilesBetween(TileNode from, TileNode to)
        {
            List<TileNode> nodes = new();
            //UNDONE
            return nodes;

        }
    }
}