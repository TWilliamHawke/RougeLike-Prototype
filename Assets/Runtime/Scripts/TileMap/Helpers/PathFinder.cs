using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using System.Linq;

namespace Entities.Behavior
{
    public class PathFinder
    {
        static TileNode _startNode;
        static TileNode _targetNode;
        static TilesGrid _mapController;

        static HashSet<TileNode> _sortedNodes = new HashSet<TileNode>();
        static HashSet<TileNode> _unsortedNodes = new HashSet<TileNode>();

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

            _startNode.targetDist = _startNode.GetDistanceFrom(_targetNode);
            _startNode.startDist = 0;
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

                if (NodeIsNeightborOfTarget(nearestNode))
                {
                    _targetNode.parent = nearestNode;
                    return;
                }

                var neightborNodes = _mapController.GetEmptyNeighbors(nearestNode);

                foreach (var node in neightborNodes)
                {
                    if (_unsortedNodes.Contains(node) || _sortedNodes.Contains(node)) continue;

                    node.parent = nearestNode;

                    node.startDist = node.GetDistanceFrom(_startNode);
                    node.targetDist = node.GetDistanceFrom(_targetNode);
                    _unsortedNodes.Add(node);
                }

                _unsortedNodes.Remove(nearestNode);
                _sortedNodes.Add(nearestNode);
            }
        }

        private bool NodeIsNeightborOfTarget(TileNode nearestNode)
        {
            return nearestNode.targetDist < TileNode.maxNeightborDistance;
        }

        TileNode FindNearestNodeFromUnsorted()
        {
            TileNode nearestNode = _unsortedNodes.FirstOrDefault();

            foreach (var node in _unsortedNodes)
            {
                try
                {
                    if (IsNodeClose(nearestNode, node))
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

        bool IsNodeClose(TileNode selectedNode, TileNode candidate)
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