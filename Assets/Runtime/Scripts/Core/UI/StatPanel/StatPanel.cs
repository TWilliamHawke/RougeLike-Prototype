using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using Entities;
using UnityEngine;
using Entities.Stats;

namespace Core.UI
{
    public class StatPanel : MonoBehaviour
    {
        [SerializeField] PlayerStats _playerStats;
        [SerializeField] StatBar _healthbar;
        [SerializeField] StatBar _manabar;

        public void SubscribeOnStatChanges()
        {
            _playerStats.AddObserver(_manabar, _manabar.observedStat);
            _playerStats.AddObserver(_healthbar, _healthbar.observedStat);
        }
    }
}