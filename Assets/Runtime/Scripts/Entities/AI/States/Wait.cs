using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

namespace Entities.AI
{
    public class Wait : IState
    {
        StateMachine _stateMachine;
        readonly int _agressionRadius = 3;
        Faction _faction;

        Vector3Int postition => _stateMachine.transform.position.ToInt();

        [InjectField] TilesGrid _tilesGrid;

        public Wait(StateMachine stateMachine, TilesGrid tilesGrid)
        {
            _stateMachine = stateMachine;
            _tilesGrid = tilesGrid;
        }

        public void StartTurn()
        {
            _stateMachine.EndTurn();
        }

        public void EndTurn()
        {

        }

        public bool Condition()
        {
            var neightBorNodes = _tilesGrid.GetNonEmptyNeighbors(postition, _agressionRadius);
            foreach (var node in neightBorNodes)
            {
                var target = node.entityInthisNode as IFactionMember;
                if (target is null) continue;
                if(target.faction.IsAgressiveToward(_stateMachine.faction)) return false;
            }

            return true;
        }
    }
}