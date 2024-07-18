using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;
using Entities.PlayerScripts;

namespace Core.UI
{
    public class ActiveEffectsPanel : UIPanelWithGrid<TemporaryEffectData>
    {
        [SerializeField] PlayerStats _playerStats;
        protected override IEnumerable<TemporaryEffectData> _layoutElementsData => _effectsStorage.temporaryEffects;

        [InjectField] Player _player;

        EffectsStorage _effectsStorage;

        public void Subscribe()
        {
            _effectsStorage = _player.GetComponent<EffectsStorage>();
			_effectsStorage.OnEffectsUpdate += UpdateLayout;
			UpdateLayout();
        }

        private void OnDestroy()
        {
			_effectsStorage.OnEffectsUpdate -= UpdateLayout;
        }
    }
}