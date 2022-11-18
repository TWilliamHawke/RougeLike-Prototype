using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

namespace Entities.Behavior
{
    public class PathFinder
    {
        static TileNode _startNode;
        static TileNode _targetNode;
        static TilesGrid _mapController;

        static List<TileNode> _sortedNodes = new List<TileNode>();
        static List<TileNode> _unsortedNodes = new List<TileNode>();

        public PathFinder(TilesGrid mapController)
        {
            _mapController = mapController;
        }

        public Stack<TileNode> FindPath(TileNode from, TileNode to)
        {
            var path = new Stack<TileNode>();
            _startNode = from;
            _targetNode = to;
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

                    if (path.Count > 300)
                    {
                        Debug.LogError("Something goes wrong!!! Path is too long!!");
                        break;
                    }
                }
            }

            _unsortedNodes.Clear();
            _sortedNodes.Clear();

            return path;
        }

        void CheckNodes()
        {
            while (_unsortedNodes.Count > 0)
            {
                var nearestNode = FindNearestNodeFromUnsorted();
                var neightborNodes = _mapController.GetEmptyNeighbors(nearestNode);

                foreach (var node in neightborNodes)
                {
                    if (_unsortedNodes.Contains(node) || _sortedNodes.Contains(node))
                    {
                        continue;
                    }

                    node.parent = nearestNode;
                    if (node == _targetNode) return;

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

                    Debug.Log(nearestNode?.position);
                    Debug.Log(node?.position);
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