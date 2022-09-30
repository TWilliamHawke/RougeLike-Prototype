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
        protected override IEnumerable<TemporaryEffectData> _layoutElementsData => _playerStats.effectStorage.temporaryEffects;

        public void Init()
        {
			_playerStats.effectStorage.OnEffectsUpdate += UpdateLayout;
			UpdateLayout();
        }

        private void OnDestroy()
        {
			_playerStats.effectStorage.OnEffectsUpdate -= UpdateLayout;
        }


    }
}