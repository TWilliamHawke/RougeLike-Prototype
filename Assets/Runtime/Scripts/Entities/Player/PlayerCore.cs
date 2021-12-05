using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using Entities.Behavior;
using Entities.Combat;
using UnityEngine.Events;
using Effects;

namespace Entities.Player
{
    [RequireComponent(typeof(VisibilityController))]
    public class PlayerCore : MonoBehaviour, IAttackTarget, ICanAttack, IEffectTarget
    {
        public event UnityAction OnPlayerTurnEnd;

        [SerializeField] TilemapController _mapController;
        [SerializeField] PlayerStats _stats;
        [SerializeField] MovementController _movementController;
        [SerializeField] MeleeAttackController _meleeAttackController;

        TileNode _currentNode;
        IInteractive _target;

        public TileNode currentNode => _currentNode;
        public Dictionary<DamageType, int> resists => _stats.CalculateCurrentResists();
        public IDamageSource damageSource => _stats.CalculateDamageData();


        public void Init()
        {
            GetComponent<VisibilityController>().ChangeViewingRange();
            _movementController.OnStepEnd += EndPlayerTurn;
            _meleeAttackController.OnAttackEnd += EndPlayerTurn;

            //stats requires player.transform
            _stats.player = this;
            
            _meleeAttackController.Init(this);
        }

        public void StartTurn()
        {
            if (_target != null && _movementController.pathLength <= 1)
            {
                _target.Interact(this);
                _target = null;
                _movementController.ClearPath();
            }
            else
            {
                _movementController.TakeAStep();
            }
        }

        public void Attack(IAttackTarget target)
        {
            _meleeAttackController.StartAttack(target);
        }

        public void GotoRemoteTarget(IInteractive target)
        {
            _target = target;
            _movementController.SetDestination(target);
            _movementController.TakeAStep();
        }

        public void GotoNode(TileNode node)
        {
            _movementController.SetDestination(node);
            _movementController.TakeAStep();
        }

        public void SpawnAt(TileNode node)
        {
            Vector3 position = node.position2d.AddZ(0);
            transform.position = position;
            _currentNode = node;
            _movementController.Init(node);
        }

        void IAttackTarget.TakeDamage(int damage)
        {
            _stats.DamageHealth(damage);
        }

        void EndPlayerTurn()
        {
            OnPlayerTurnEnd?.Invoke();
        }

    }
}