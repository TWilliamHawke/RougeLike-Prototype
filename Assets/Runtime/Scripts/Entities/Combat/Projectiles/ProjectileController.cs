using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;
using Map;

namespace Entities.Combat
{
    public class ProjectileController : MonoBehaviour, IInjectionTarget
    {
        static ObjectPool<Projectile> _projectiles;
        static ObjectPool<AoeAnimation> _aoeEffects;

        public event UnityAction OnAttackEnd;
        [SerializeField] ProjectileTemplate _testTemplate;
        [SerializeField] Projectile _projectilePrefab;
        [SerializeField] AoeAnimation _aoeEffectPrefab;
        [SerializeField] Injector _tilesGridInjector;

        [InjectField] TilesGrid _tilesGrid;

        const float BASE_SPEED = 5;

        IRangeAttackTarget _target;
        Vector3 _targetPosition;
        float _targetProgress;
        float _progress;

        Projectile _launchedProjectile;
        AoeAnimation _startedAoeEffect;
        Coroutine _projectileCoroutine;

        public bool waitForAllDependencies => false;

        private void Awake()
        {
            _tilesGridInjector.AddInjectionTarget(this);
            FillProjectilesPool();
        }

        void Update()
        {
            if (_target is null) return;

            if (_progress < _targetProgress)
            {
                _progress += Time.deltaTime * BASE_SPEED * _launchedProjectile.speed;
                var newPos = Vector3.Lerp(transform.position, _targetPosition, _progress / _targetProgress);
                _launchedProjectile.transform.position = newPos;
            }
            else
            {
                DoDamage();
                _target = null;
            }
        }

        public void ThrowProjectile(IRangeAttackTarget target, ProjectileTemplate template)
        {
            this.TryStopCoroutine(_projectileCoroutine);
            TryReleaseProjectile();
            _target = target;
            _targetPosition = target.transform.position;
            _targetProgress = Vector3.Distance(transform.position, _targetPosition);

            _launchedProjectile = _projectiles.Get();
            _launchedProjectile.transform.position = transform.position;
            _launchedProjectile.transform.right = target.transform.position - transform.position;
            _launchedProjectile.SetTemplate(template);
            _launchedProjectile.PlayFireSound();
        }

        public void ThrowProjectile(IRangeAttackTarget target)
        {
            ThrowProjectile(target, _testTemplate);
        }

        private void DoDamage()
        {
            _progress = 0;
            AttackHandler.ProcessAttack(_launchedProjectile.template, _target);
            _launchedProjectile.PlayImpactSound();
            _launchedProjectile.HideSprite();
            _projectileCoroutine = StartCoroutine(ReleaseAfter5Sec());

            if (_launchedProjectile.template.radius < 1)
            {
                OnAttackEnd?.Invoke();
            }
            else
            {
                StartAoeAnimation();
            }
        }

        private void StartAoeAnimation()
        {
            TryReleaseAOE();
            _startedAoeEffect = _aoeEffects.Get();
            _startedAoeEffect.transform.position = _target.transform.position;
            _startedAoeEffect.SetTemplate(_launchedProjectile.template);
            _startedAoeEffect.OnAnimationEnd += FinalizeAOE;
            _startedAoeEffect.OnDamageFrame += DoAoeDamage;
        }

        private void DoAoeDamage(int radius)
        {
            if (_startedAoeEffect is null) return;
            var neightBorNodes = _tilesGrid.GetNonEmptyNeighbors(_startedAoeEffect.tilepos);

            foreach (var node in neightBorNodes)
            {
                var target = node.entityInthisNode as IRangeAttackTarget;
                if (target is null) continue;
                AttackHandler.ProcessAttack(_startedAoeEffect.template, target);
            }
        }

        private void FillProjectilesPool()
        {
            if (_projectiles is not null) return;

            _projectiles = new ObjectPool<Projectile>(
                createFunc: () => Instantiate(_projectilePrefab),
                actionOnGet: proj => proj.SetParent(this)
            );

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

        private void FinalizeAOE()
        {
            _startedAoeEffect.OnAnimationEnd -= FinalizeAOE;
            _startedAoeEffect.OnDamageFrame -= DoAoeDamage;
            OnAttackEnd?.Invoke();
            TryReleaseAOE();
        }

        private IEnumerator ReleaseAfter5Sec()
        {
            yield return new WaitForSeconds(5f);
            TryReleaseProjectile();
        }

        private void TryReleaseProjectile()
        {
            if (_launchedProjectile is null) return;
            _projectiles.Release(_launchedProjectile);
            _launchedProjectile = null;
        }

        private void TryReleaseAOE()
        {
            if (_startedAoeEffect is null) return;
            _aoeEffects.Release(_startedAoeEffect);
            _startedAoeEffect = null;
        }

        void IInjectionTarget.FinalizeInjection()
        {
        }
    }
}