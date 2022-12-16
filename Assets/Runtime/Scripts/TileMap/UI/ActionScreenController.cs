using System.Collections;
using System.Collections.Generic;
using Map.Objects;
using UnityEngine;

namespace Map.UI
{
    public class ActionScreenController : MonoBehaviour, IActionScreenController
    {
        [SerializeField] ActionsScreen _actionsScreen;
        [SerializeField] Injector _thisInjector;

        [InjectField] MapObjectObserver _mapObjectObserver;

        IMapObject _currentMapObject;

        private void Awake()
        {
            _thisInjector.SetDependency(this);
        }

        //used in editor
        public void FinalizeInjection()
        {
            _mapObjectObserver.OnMapObjectChange += ChangeTargetMapObject;
            _currentMapObject = _mapObjectObserver.currentMapObject;
        }

        //used in editor
        public void OpenActionScreen()
        {
            if (_currentMapObject is null || _currentMapObject.mapActionList.count == 0) return;
            _actionsScreen.SetTitle(_currentMapObject.displayName);
            _actionsScreen.SetIcon(_currentMapObject.icon);
            _actionsScreen.SetActions(_currentMapObject.mapActionList);
            _actionsScreen.Open();
        }

        public void CloseActionScreen()
        {
            _actionsScreen.Close();
        }

        private void ChangeTargetMapObject(IMapObject mapObject)
        {
            _currentMapObject = mapObject;
        }

    }
}


