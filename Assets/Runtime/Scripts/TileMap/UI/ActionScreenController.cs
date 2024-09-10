using System.Collections;
using System.Collections.Generic;
using Map.Zones;
using UnityEngine;

namespace Map.UI
{
    public class ActionScreenController : MonoBehaviour, IActionScreenController
    {
        [SerializeField] ActionsScreen _actionsScreen;
        [SerializeField] Injector _thisInjector;

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
            if (_currentZone is null || _currentZone.mapActionList.count == 0) return;
            _actionsScreen.SetTitle(_currentZone.displayName);
            _actionsScreen.SetIcon(_currentZone.icon);

            _actionsScreen.SetActions(_currentZone.mapActionList);
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


