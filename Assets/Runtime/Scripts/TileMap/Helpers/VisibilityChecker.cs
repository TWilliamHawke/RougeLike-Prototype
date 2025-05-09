using System.Collections.Generic;

namespace Map.Helpers
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