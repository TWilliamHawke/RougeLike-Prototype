using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class OpenModalWindow : ContextActionContainer
    {
        ModalWindowController _modalWindow;
        ModalWindowData _modalWindowData;
        protected string _actionTitle = "Open Modal Window";

        public OpenModalWindow(ModalWindowController modalWindow, ModalWindowData modalWindowData)
        {
            _modalWindow = modalWindow;
            _modalWindowData = modalWindowData;
        }

        public override void DoAction()
		{
            _modalWindow.OpenWindow(_modalWindowData);
		}
    }
}


