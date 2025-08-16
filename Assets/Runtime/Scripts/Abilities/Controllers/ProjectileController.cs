using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;
using Entities;

namespace Abilities
{
    public class ProjectileController : MonoBehaviour
    {
        ObjectPool<Projectile> _projectiles;

        public event UnityAction OnAttackEnd;
        [SerializeField] ProjectileTemplate _testTemplate;
        [SerializeField] Projectile _projectilePrefab;

        [InjectField] AoeAbilityController _aoeController;

        const float BASE_SPEED = 5;

        IAbilityTarget _target;
        Vector3 _targetPosition;
        float _targetProgress;
        float _progress;

        ProjectileAbility _selectedAbility;

        Projectile _launchedProjectile;

        private void Awake()
        {
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

        public void ThrowProjectile(IAbilityTarget target, ProjectileTemplate template)
        {
            TryReleaseProjectile();
            var positionController = target.GetEntityComponent<PositionController>();
            _target = target;
            _targetPosition = positionController.position;
            _targetProgress = Vector3.Distance(transform.position, _targetPosition);

            _launchedProjectile = _projectiles.Get();
            _launchedProjectile.transform.position = transform.position;
            _launchedProjectile.transform.right = positionController.position - transform.position;
            _launchedProjectile.SetTemplate(template);
            _launchedProjectile.PlayFireSound();
        }

        public void ThrowProjectile(IAbilityTarget target)
        {
            ThrowProjectile(target, _testTemplate);
        }

        private void DoDamage()
        {
            _progress = 0;
            _selectedAbility.ApplyEffect(_target);
            _selectedAbility.PlayImpactSound();
            _launchedProjectile.HideSprite();
            TryReleaseProjectile();

            if (_launchedProjectile.template.radius < 1)
            {
                OnAttackEnd?.Invoke();
            }
            else
            {
                _aoeController.StartAoeAnimation(_launchedProjectile.template, _target);
            }
        }

        private void FillProjectilesPool()
        {
            if (_projectiles is not null) return;

            _projectiles = new ObjectPool<Projectile>(
                createFunc: () => Instantiate(_projectilePrefab),
                actionOnGet: proj => proj.SetParent(this)
            );
        }

        private void TryReleaseProjectile()
        {
            if (_launchedProjectile is null) return;
            _projectiles.Release(_launchedProjectile);
            _launchedProjectile = null;
        }
    }
}