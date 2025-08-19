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
                UpdateProgress();
            }
            else
            {
                FinishAbility();
            }
        }

        public void UseAbility(IAbilityTarget target, ProjectileAbility ability)
        {
            TryReleaseProjectile();
            var targetPos = target.GetEntityComponent<PositionController>();
            _target = target;
            _selectedAbility = ability;
            _targetPosition = targetPos.position;
            _targetProgress = Vector3.Distance(ability.userPosition, _targetPosition);

            _launchedProjectile = _projectiles.Get();
            _launchedProjectile.MoveTo(ability.userPosition);
            _launchedProjectile.RotateTo(targetPos.position - ability.userPosition);
            _launchedProjectile.SetTemplate(ability.projectileTemplate);
            _launchedProjectile.PlayFireSound();
        }

        private void UpdateProgress()
        {
            _progress += Time.deltaTime * BASE_SPEED * _launchedProjectile.speed;
            var newPos = Vector3.Lerp(_selectedAbility.userPosition, _targetPosition, _progress / _targetProgress);
            _launchedProjectile.MoveTo(newPos);
        }

        private void FinishAbility()
        {
            _progress = 0;
            _selectedAbility.ApplyEffect(_target);
            _selectedAbility.PlayImpactSound();

            if (_launchedProjectile.template.radius < 1)
            {
                OnAttackEnd?.Invoke();
            }
            else
            {
                _aoeController.StartAoeAnimation(_selectedAbility.abilityTemplate, _target);
            }

            TryReleaseProjectile();
            _target = null;
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
            _launchedProjectile.HideSprite();
            _projectiles.Release(_launchedProjectile);
            _launchedProjectile = null;
        }
    }
}