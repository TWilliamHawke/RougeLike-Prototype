using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class OpenModalWindow : IContextAction
    {
        public string actionTitle => "Open Modal Window";
		IModalWindow _modalWindow;
        ModalWindowData _modalWindowData;

        public OpenModalWindow(IModalWindow modalWindow, ModalWindowData modalWindowData)
        {
            _modalWindow = modalWindow;
            _modalWindowData = modalWindowData;
        }


        void DoAction()
		{
			_modalWindow.Open(_modalWindowData);
		}
    }
}


