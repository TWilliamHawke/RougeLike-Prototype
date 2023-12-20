using System.Collections;
using System.Collections.Generic;
using Core;
using Entities.Behavior;
using Entities.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Entities.UI
{
    public class Healthbar : MonoBehaviour, IInjectionTarget
    {
        IHealthbarData _healthbarData;
        IStatValues _healthStat;

        [SerializeField] Injector _mainCameraInjector;
        [SerializeField] Image _fillImage;
        [SerializeField] Vector3 _shift;
        [Range(0, 0.1f)]
        [SerializeField] float _minVisibleHealth = 0.05f;
        [Header("Colors")]
        [SerializeField] Color _playerColor = Color.green;
        [SerializeField] Color _friendlyColor = Color.yellow;
        [SerializeField] Color _neutralColor = Color.blue;
        [SerializeField] Color _enemyColor = Color.red;

        [InjectField] Camera _mainCamera;

        public bool waitForAllDependencies => false;

        private void Awake()
        {
            _mainCameraInjector.AddInjectionTarget(this);
        }

        public void BindHealth(IHealthbarData entity, IStatValues healthStat)
        {
            _healthbarData = entity;
            _healthStat = healthStat;
            _healthStat.OnValueChange += UpdateHealthBar;
            UpdateHealthBar();
        }

        public void SubscribeOnFactionEvent(IFactionMember factionMember)
        {
            UpdateHealthColor(factionMember.faction);
            factionMember.OnFactionChange += UpdateHealthColor;
        }

        private void UpdateHealthColor(Faction faction)
        {
            _fillImage.color = GetHealthbarColor(faction.GetAntiPlayerBehavior());
        }

        void UpdateHealthBar(int _ = 0)
        {
            float healthPct = (float)_healthStat.currentValue / _healthStat.maxValue;
            _fillImage.fillAmount = Mathf.Clamp(healthPct, _minVisibleHealth, 1);
        }

        Color GetHealthbarColor(BehaviorType behavior)
        {
            return behavior switch
            {
                BehaviorType.none => _playerColor,
                BehaviorType.agressive => _enemyColor,
                BehaviorType.neutral => _neutralColor,
                _ => _friendlyColor
            };
        }

        void LateUpdate()
        {
            if (_mainCamera is null) return;
            if (_healthbarData is null)
            {
                DestroyImmediate(this);
            }
            else
            {
                var entityPos = _mainCamera.WorldToScreenPoint(_healthbarData.bodyPosition + _shift);
                transform.position = entityPos;
            }
        }

        public void FinalizeInjection()
        {
        }
    }
}