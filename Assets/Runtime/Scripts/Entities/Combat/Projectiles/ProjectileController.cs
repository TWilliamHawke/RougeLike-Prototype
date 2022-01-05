using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Combat
{
    public class ProjectileController : MonoBehaviour
    {
        public event UnityAction OnAttackEnd;

        [SerializeField] ProjectileTemplate _testTemplate;
        [SerializeField] Projectile _projectilePrefab;

        float _baseSpeed = 5;

        IRangeAttackTarget _target;
        Vector3 _targetPosition;
        float _targetProgress;
        float _progress;

        Projectile _launchedProjectile;

        void Update()
        {
            if (_launchedProjectile == null) return;

            if (_progress < _targetProgress)
            {
                _progress += Time.deltaTime * _baseSpeed * _launchedProjectile.speed;
                var newPos = Vector3.Lerp(transform.position, _targetPosition, _progress / _targetProgress);
                _launchedProjectile.transform.position = newPos;
            }
            else
            {
                Damage();
            }
        }

        private void Damage()
        {
            _progress = 0;
            var damage = DamageCalulator.GetDamage(_launchedProjectile.template, _target);
            _target.TakeDamage(damage);
            _launchedProjectile.PlayImpactSound();
            _launchedProjectile.Hide();

            Destroy(_launchedProjectile.gameObject, 5f);
            _launchedProjectile = null;
            OnAttackEnd?.Invoke();
        }

        public void ThrowProjectile(IRangeAttackTarget target, ProjectileTemplate template)
        {
            _target = target;
            _targetPosition = target.transform.position;
            _targetProgress = Vector3.Distance(transform.position, _targetPosition);

            _launchedProjectile = Instantiate(_projectilePrefab, transform);
            _launchedProjectile.transform.right = target.transform.position - transform.position;
            _launchedProjectile.SetTemplate(template);
            _launchedProjectile.PlaySound(template.fireSounds.GetRandom());

        }

        public void ThrowProjectile(IRangeAttackTarget target)
        {
            ThrowProjectile(target, _testTemplate);
        }
    }
}