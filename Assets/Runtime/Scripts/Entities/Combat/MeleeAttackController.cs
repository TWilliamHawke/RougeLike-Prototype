using System.Collections;
using System.Collections.Generic;
using Core.Settings;
using UnityEngine;
using Core.Input;
using UnityEngine.Events;

namespace Entities.Combat
{
    public class MeleeAttackController : MonoBehaviour, IInjectionTarget
    {
        public event UnityAction OnAttackEnd;

        [SerializeField] GlobalSettings _settings;
        [SerializeField] Injector _inputControllerInjector;
        [SerializeField] AudioSource _body;

        [InjectField] InputController _inputController;

        ICanAttack _attacker;
        IAttackTarget _target;

        Vector3 _defaultPosition;
        Vector3 _attackPosition;
        AttackPhases _attackPhase = AttackPhases.none;
        float _attackProgress;
        float _directionMult = 1;

        public bool isAttack => _attackPhase != AttackPhases.none;

        bool IInjectionTarget.waitForAllDependencies => false;

        void Awake()
        {
            _inputControllerInjector.AddInjectionTarget(this);
        }

        private void Update()
        {
            if (_attackPhase == AttackPhases.none) return;

            _attackProgress += Time.deltaTime * (int)_attackPhase * _settings.animationSpeed * _directionMult;
            _body.transform.position = Vector3.Lerp(_defaultPosition, _attackPosition, _attackProgress);

            if (_attackProgress >= 1 && _attackPhase == AttackPhases.moveTo)
            {
                _attackPhase = AttackPhases.moveAway;
                DoDamage(_attacker.damageSource, _target);
            }

            if (_attackProgress <= 0 && _attackPhase == AttackPhases.moveAway)
            {
                _attackProgress = 0;
                _body.transform.position = _defaultPosition;
                _attackPhase = AttackPhases.none;
                OnAttackEnd?.Invoke();
            }
        }

        public void Init(ICanAttack attacker)
        {
            _attacker = attacker;
        }

        public void StartAttack(IAttackTarget target)
        {
            _target = target;
            _attackPosition = (transform.position + target.transform.position) * 0.5f;
            _defaultPosition = transform.position;

            var distance = Vector3.Distance(transform.position, _attackPosition);
            _directionMult = 1 / distance; //diagonal 1.4 times faster

            _attacker.PlayAttackSound();

            _attackPhase = AttackPhases.moveTo;
            _inputController.DisableLeftClick();  //HACK this should be in entityController
        }

        void DoDamage(IDamageSource damageSource, IAttackTarget target)
        {
            int damage = DamageCalulator.GetDamage(damageSource, target);
            target.TakeDamage(damage);
        }

        public void FinalizeInjection()
        {
            
        }

        enum AttackPhases
        {
            none = 0,
            moveTo = 1,
            moveAway = -1
        }
    }
}