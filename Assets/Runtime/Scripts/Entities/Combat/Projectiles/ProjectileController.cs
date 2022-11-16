using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;

namespace Entities.Combat
{
    public class ProjectileController : MonoBehaviour
    {
        static ObjectPool<Projectile> _projectiles;
        static ObjectPool<AOEEffect> _aoeEffects;

        public event UnityAction OnAttackEnd;
        [SerializeField] ProjectileTemplate _testTemplate;
        [SerializeField] Projectile _projectilePrefab;
        [SerializeField] AOEEffect _aoeEffectPrefab;

        const float BASE_SPEED = 5;

        IRangeAttackTarget _target;
        Vector3 _targetPosition;
        float _targetProgress;
        float _progress;

        Projectile _launchedProjectile;
        AOEEffect _startedAoeEffect;
        Coroutine _projectileCoroutine;

        private void Awake()
        {
            if (_projectiles is not null) return;

            _projectiles = new ObjectPool<Projectile>(
                createFunc: () => Instantiate(_projectilePrefab),
                actionOnGet: proj => proj.SetParent(this)
            );

            _aoeEffects = new ObjectPool<AOEEffect>(
                createFunc: () => Instantiate(_aoeEffectPrefab),
                actionOnGet: aoe =>
                {
                    aoe.transform.SetParent(this.transform);
                    aoe.Reset();
                },
                actionOnRelease: aoe => aoe.Hide()
            );
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
            }
        }

        private void DoDamage()
        {
            _progress = 0;
            var damage = DamageCalulator.GetDamage(_launchedProjectile.template, _target);
            _target.TakeDamage(damage);
            _target = null;
            _launchedProjectile.PlayImpactSound();
            _launchedProjectile.Hide();
            _projectileCoroutine = StartCoroutine(ReleaseAfter5Sec());

            if (_launchedProjectile.template.radius < 1)
            {
                OnAttackEnd?.Invoke();
            }
            else
            {
                TryReleaseAOE();
                _startedAoeEffect = _aoeEffects.Get();
                _startedAoeEffect.SetTemplate(_launchedProjectile.template);
                _startedAoeEffect.OnAnimationEnd += FinalizeAOE;
            }
        }

        public void ThrowProjectile(IRangeAttackTarget target, ProjectileTemplate template)
        {
            if (_projectileCoroutine is not null)
            {
                StopCoroutine(_projectileCoroutine);
            }
            TryReleaseProjectile();
            _target = target;
            _targetPosition = target.transform.position;
            _targetProgress = Vector3.Distance(transform.position, _targetPosition);

            _launchedProjectile = _projectiles.Get();
            _launchedProjectile.transform.position = transform.position;
            _launchedProjectile.transform.right = target.transform.position - transform.position;
            _launchedProjectile.SetTemplate(template);
            _launchedProjectile.PlaySound(template.fireSounds.GetRandom());
        }

        public void ThrowProjectile(IRangeAttackTarget target)
        {
            ThrowProjectile(target, _testTemplate);
        }

        IEnumerator ReleaseAfter5Sec()
        {
            yield return new WaitForSeconds(5f);
            TryReleaseProjectile();
        }

        void TryReleaseProjectile()
        {
            if (_launchedProjectile is null) return;
            _projectiles.Release(_launchedProjectile);
            _launchedProjectile = null;
        }

        void TryReleaseAOE()
        {
            if (_startedAoeEffect is null) return;
            _aoeEffects.Release(_startedAoeEffect);
            _startedAoeEffect = null;
        }

        void FinalizeAOE()
        {
            _startedAoeEffect.OnAnimationEnd -= FinalizeAOE;
            OnAttackEnd?.Invoke();
            TryReleaseAOE();
        }
    }
}