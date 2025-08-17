using System.Linq;
using Entities;
using Entities.Combat;
using Map;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace Abilities
{
    public class AoeAbilityController : MonoBehaviour
    {
        ObjectPool<AoeAnimation> _aoeEffects;
        AoeAnimation _startedAoeEffect;
        IAbilityTarget _target;
        public event UnityAction OnAttackEnd;

        [SerializeField] AoeAnimation _aoeEffectPrefab;

        [InjectField] TilesGrid _tilesGrid;

        void Awake()
        {
            FillAoePool();
        }

        public void StartAoeAnimation(ProjectileAbilityTemplate template, IAbilityTarget target)
        {
            _target = target;
            var positionController = _target.GetEntityComponent<PositionController>();
            TryReleaseAOE();
            _startedAoeEffect = _aoeEffects.Get();
            _startedAoeEffect.transform.position = positionController.position;
            _startedAoeEffect.SetTemplate(template);
            _startedAoeEffect.OnAnimationEnd += FinalizeAOE;
            _startedAoeEffect.OnDamageFrame += DoAoeDamage;
        }

        private void FillAoePool()
        {
            _aoeEffects = new ObjectPool<AoeAnimation>(
                createFunc: () => Instantiate(_aoeEffectPrefab),
                actionOnGet: aoe =>
                {
                    aoe.transform.SetParent(this.transform);
                    aoe.Reset();
                },
                actionOnRelease: aoe => aoe.Hide()
            );
        }

        private void DoAoeDamage(int radius)
        {
            if (_startedAoeEffect is null) return;
            var neightBorNodes = _tilesGrid.GetNonEmptyNeighbors(_startedAoeEffect.tilepos);

            foreach (var node in neightBorNodes)
            {
                var entity = node.entitiesOnTile.FirstOrDefault();
                var target = entity as IAbilityTarget;
                if (target is null) continue;
                _startedAoeEffect.ApplyEffect(target);
            }
        }

        private void FinalizeAOE()
        {
            //TODO fix bug with multiple projectiles
            _startedAoeEffect.OnAnimationEnd -= FinalizeAOE;
            _startedAoeEffect.OnDamageFrame -= DoAoeDamage;
            OnAttackEnd?.Invoke();
            TryReleaseAOE();
        }

        private void TryReleaseAOE()
        {
            if (_startedAoeEffect is null) return;
            _aoeEffects.Release(_startedAoeEffect);
            _startedAoeEffect = null;
        }


    }
}