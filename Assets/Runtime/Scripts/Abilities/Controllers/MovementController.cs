using System.Collections.Generic;
using Core.Settings;
using Map;
using UnityEngine;
using Core.Input;
using UnityEngine.Events;
using Entities;

namespace Abilities
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] GlobalSettings _settings;
        [SerializeField] StepSoundKit _stepSounds;

        [InjectField] TilesGrid _tileGrid;

        MovementAbility _activeAbility;

        void Update()
        {
            if (_activeAbility is null) return;
            if (_activeAbility.onPause) return;
            float deltaTime = Time.deltaTime * _settings.animationSpeed;
            _activeAbility.UpdateProgress(deltaTime);

            if (_activeAbility.progress >= 1)
            {
                _activeAbility.FinalizeStep();
            }
        }

        public void SelectActiveAbility(MovementAbility ability)
        {
            _activeAbility = ability;
        }

        public Stack<TileNode> FindPath(Vector3Int from, Vector3Int to)
        {
            return _tileGrid.FindPath(from, to);
        }

        public TileNode FindNode(Vector3 position)
        {
            return _tileGrid.GetNode(position.ToInt());
        }

        void PlayStepSound()
        {
            var clip = _stepSounds.GetRandom();
            //_body.PlaySound(clip);
        }
    }
}