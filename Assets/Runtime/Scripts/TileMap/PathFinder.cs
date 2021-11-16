using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
	public class PathFinder
	{
		TileNode _startNode;
        TileNode _targetNode;
		TileGrid _grid;

        List<TileNode> _sortedNodes = new List<TileNode>();
        List<TileNode> _unsortedNodes = new List<TileNode>();

        public PathFinder(TileNode startNode, TileNode targetNode, TileGrid grid)
        {
            _startNode = startNode;
            _targetNode = targetNode;

            _grid = grid;
        }

        public Stack<TileNode> GetPath()
        {
            var path = new Stack<TileNode>();
            _targetNode.parent = null;

            _unsortedNodes.Add(_startNode);

            CheckNodes();

            if (_targetNode.parent != null)
            {
                var pathPoint = _targetNode;
                while (pathPoint != _startNode)
                {
                    path.Push(pathPoint);
                    pathPoint = pathPoint.parent;

                    if (path.Count > 80)
                    {
                        Debug.LogError("Something goes wrong!!! Path is too long!!");
                        break;
                    }
                }
            }

            return path;
        }

        void CheckNodes()
        {
            while (_unsortedNodes.Count > 0)
            {
                var nearestNode = FindNearestNodeFromUnsorted();
                var neightborNodes = _grid.GetNeighbors(nearestNode);

                foreach (var node in neightborNodes)
                {
                    if (_unsortedNodes.Contains(node) || _sortedNodes.Contains(node))
                    {
                        continue;
                    }
                    node.parent = nearestNode;
                    if (node == _targetNode)
                    {
                        return;
                    }

                    node.startDist = node.GetDistanceFrom(_startNode);
                    node.targetDist = node.GetDistanceFrom(_targetNode);
                    _unsortedNodes.Add(node);
                }

                _unsortedNodes.Remove(nearestNode);
                _sortedNodes.Add(nearestNode);
            }
        }

        TileNode FindNearestNodeFromUnsorted()
        {
            TileNode nearestNode = _unsortedNodes[0];

            foreach (var node in _unsortedNodes)
            {
                try
				{
					 if (IsnodeClose(nearestNode, node))
                {
                    nearestNode = node;
                }
				}
				catch (System.Exception)
				{
					
					Debug.Log(nearestNode?.position2d);
					Debug.Log(node?.position2d);
					
				}
            }
            return nearestNode;
        }

        bool IsnodeClose(TileNode selectedNode, TileNode candidate)
        {
            if (selectedNode.totalDist < candidate.totalDist)
            {
                return false;
            }
            if (selectedNode.totalDist == candidate.totalDist && selectedNode.targetDist < candidate.targetDist)
            {
                return false;
            }
            return true;
        }
	}
}