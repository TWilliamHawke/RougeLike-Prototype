using Core.Settings;
using UnityEngine;
using UnityEngine.Events;
using Entities;
using Map;

namespace Abilities
{
    public class MeleeAttackController : MonoBehaviour
    {
        public event UnityAction OnAttackEnd;

        [SerializeField] GlobalSettings _settings;
        [InjectField] TilesGrid _tilemapController;

        IPositionData _target;

        Vector3 _turnBackPosition;
        AttackPhases _attackPhase = AttackPhases.none;
        float _attackProgress;
        float _directionMult = 1;
        MeleeAbility _selectedAbility;

        Vector3 _attackerPosition => _selectedAbility.userPosition;
        float animationSpeed => _settings.animationSpeed;

        private void Update()
        {
            if (_attackPhase == AttackPhases.none) return;
            if (_selectedAbility == null) return;

            UpdateProgress();
            TryApplyEffect();
            TryFinishAbility();
        }

        public void UseAbility(IAbilityTarget target, MeleeAbility ability)
        {
            ability.PlayAttackSound();
            _selectedAbility = ability;
            _target = target.GetEntityComponent<PositionController>();
            _turnBackPosition = (_attackerPosition + _target.position) * 0.5f;

            var distance = Vector3.Distance(_attackerPosition, _turnBackPosition);
            _directionMult = 1 / distance; //diagonal 1.4 times faster

            _attackPhase = AttackPhases.moveTo;
        }

        private void TryApplyEffect()
        {
            if (_attackProgress >= 1 && _attackPhase == AttackPhases.moveTo)
            {
                _attackPhase = AttackPhases.moveAway;
                _selectedAbility.ApplyEffect(_target.position, _tilemapController);
            }
        }

        private void TryFinishAbility()
        {
            if (_attackProgress <= 0 && _attackPhase == AttackPhases.moveAway)
            {
                _attackProgress = 0;
                _selectedAbility.MoveUserBody(_attackerPosition);
                _attackPhase = AttackPhases.none;
                OnAttackEnd?.Invoke();
            }
        }

        private void UpdateProgress()
        {
            _attackProgress += Time.deltaTime * (int)_attackPhase * animationSpeed * _directionMult;
            var newBodyPosition = Vector3.Lerp(_attackerPosition, _turnBackPosition, _attackProgress);
            _selectedAbility.MoveUserBody(newBodyPosition);
        }
        enum AttackPhases
        {
            none = 0,
            moveTo = 1,
            moveAway = -1
        }
    }
}