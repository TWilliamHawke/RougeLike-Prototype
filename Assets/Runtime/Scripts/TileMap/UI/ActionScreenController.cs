using System.Collections;
using System.Collections.Generic;
using Map.Zones;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Map.UI
{
    public class ActionScreenController : MonoBehaviour, IActionScreenController
    {
        [SerializeField] UIScreen _actionsScreen;
        [SerializeField] Injector _thisInjector;
        [Header("UI Elements")]
        [SerializeField] TextMeshProUGUI _title;
        [SerializeField] Image _zoneIcon;
        [SerializeField] ActionButtonsPanel _actionButtonsPanel;


        [InjectField] MapZonesObserver _mapZonesObserver;

        IMapZone _currentZone;

        private void Awake()
        {
            _thisInjector.SetDependency(this);
        }

        //used in editor
        public void FinalizeInjection()
        {
            _mapZonesObserver.OnMapZoneChange += ChangeTargetMapZone;
            _currentZone = _mapZonesObserver.currentMapZone;
        }

        //used in editor
        public void OpenActionScreen()
        {
            if (_currentZone is null || _currentZone.actionList.count == 0) return;
            _zoneIcon.sprite = _currentZone.icon;
            _title.text = _currentZone.displayName;
            _actionButtonsPanel.SetActions(_currentZone.actionList);

            _actionsScreen.Open();
        }

        public void CloseActionScreen()
        {
            _actionsScreen.Close();
        }

        private void ChangeTargetMapZone(IMapZone mapZone)
        {
            _currentZone = mapZone;
        }

    }
}


