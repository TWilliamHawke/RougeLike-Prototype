using System.Collections;
using System.Collections.Generic;
using Core;
using Entities.Behavior;
using Entities.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Entities.UI
{
    public class Healthbar : MonoBehaviour, IInjectionTarget, IObserver<IResourceStorageData>, IObserver<IFactionMember>
    {
        IHavePosition _body;

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

        void LateUpdate()
        {
            if (_mainCamera is null) return;
            if (_body is null)
            {
                DestroyImmediate(this);
            }
            else
            {
                var entityPos = _mainCamera.WorldToScreenPoint(_body.position + _shift);
                transform.position = entityPos;
            }
        }

        public void FollowTheBody(IHavePosition entity)
        {
            _body = entity;
        }

        public void AddToObserve(IResourceStorageData target)
        {
            target.OnValueChange += UpdateHealthBar;
            UpdateHealthBar(target.value, target.maxValue);
        }

        public void RemoveFromObserve(IResourceStorageData target)
        {
            target.OnValueChange -= UpdateHealthBar;
        }

        public void AddToObserve(IFactionMember target)
        {
            UpdateHealthColor(target.faction);
            target.OnFactionChange += UpdateHealthColor;
        }

        public void RemoveFromObserve(IFactionMember target)
        {
            target.OnFactionChange -= UpdateHealthColor;
        }

        private void UpdateHealthColor(Faction faction)
        {
            _fillImage.color = GetHealthbarColor(faction.GetAntiPlayerBehavior());
        }

        private void UpdateHealthBar(int currentValue, int maxValue)
        {
            float healthPct = (float)currentValue / maxValue;
            _fillImage.fillAmount = Mathf.Clamp(healthPct, _minVisibleHealth, 1);
        }

        private Color GetHealthbarColor(BehaviorType behavior)
        {
            return behavior switch
            {
                BehaviorType.none => _playerColor,
                BehaviorType.agressive => _enemyColor,
                BehaviorType.neutral => _neutralColor,
                _ => _friendlyColor
            };
        }

        public void FinalizeInjection()
        {
        }

    }
}