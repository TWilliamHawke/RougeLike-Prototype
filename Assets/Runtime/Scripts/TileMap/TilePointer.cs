using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Input;

namespace Map
{
    public class TilePointer : MonoBehaviour, IInjectionTarget
    {
        [SerializeField] Injector _inputControllerInjector;
        [SerializeField] Color _defaultColor = Color.yellow;
        [SerializeField] Color _enemyColor = Color.red;
        [SerializeField] SpriteRenderer _sprite;

        [InjectField] InputController _inputController;

        bool IInjectionTarget.waitForAllDependencies => false;

        void Awake()
        {
            _sprite.color = _defaultColor;
            _inputControllerInjector.AddInjectionTarget(this);
        }

        void OnDestroy()
        {
            if(_inputController is null) return;
            _inputController.OnHoveredTileChange -= ChangePosition;
        }

        void ChangePosition(Vector3Int position)
        {
            transform.position = transform.position.ReplaceXYFrom(position);
        }

        public void FinalizeInjection()
        {
            _inputController.OnHoveredTileChange += ChangePosition;
        }
    }
}