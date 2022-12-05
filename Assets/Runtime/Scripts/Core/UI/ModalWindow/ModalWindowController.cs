using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class ModalWindowController : MonoBehaviour
    {
        [SerializeField] ModalWindow _modalWindow;

        [SerializeField] Injector _selfInjector;

        private void Awake()
        {
			_selfInjector.SetDependency(this);
        }

        public void OpenWindow(ModalWindowData windowData)
        {
            _modalWindow.Open(windowData);
        }

        public bool TryCreateActionWrapper(ref IContextAction action)
        {
            var dataCreator = action as IHaveModalWindowData;
            if (dataCreator is null) return false;
            
            var data = new ModalWindowData();
            if (!dataCreator.TryFillModalWindowData(ref data)) return false;
            action = new OpenModalWindow(this, data);
            return true;
        }
    }
}


