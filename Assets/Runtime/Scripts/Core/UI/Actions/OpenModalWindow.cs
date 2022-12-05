using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class OpenModalWindow : IContextAction
    {
        public string actionTitle => "Open Modal Window";
		ModalWindowController _modalWindow;
        ModalWindowData _modalWindowData;

        public OpenModalWindow(ModalWindowController modalWindow, ModalWindowData modalWindowData)
        {
            _modalWindow = modalWindow;
            _modalWindowData = modalWindowData;
        }


        void DoAction()
		{
			_modalWindow.OpenWindow(_modalWindowData);
		}
    }
}


