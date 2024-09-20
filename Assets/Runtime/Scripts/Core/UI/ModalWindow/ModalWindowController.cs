using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class ModalWindowController : MonoBehaviour
    {
        [SerializeField] UIScreen _modalWindow;
        [SerializeField] ModalWindowComponents _components;

        IContextAction _action;

        public void OpenWindow(ModalWindowData windowData)
        {
            _action = windowData.action;
            _components.ResetAll();
            _components.ChangeTitle(windowData.title);
            TryShowText(windowData);
            TryShowImage(windowData);
            TryShowResources(windowData);
            TryChangeConfirmButton(windowData);
            _modalWindow.Open();
        }

        public void TryShowText(ModalWindowData windowData)
        {
            if (windowData.mainText == "") return;
            _components.ShowText(windowData.mainText);
        }

        public void TryShowImage(ModalWindowData windowData)
        {
            if (windowData.mainImage == null) return;
            _components.ShowImage(windowData.mainImage);
        }

        public void TryShowResources(ModalWindowData windowData)
        {
            if (windowData.resourcesData == null) return;
            _components.ShowResources(windowData.resourcesData);
        }

        public void TryChangeConfirmButton(ModalWindowData windowData)
        {
            if (windowData.action == null) return;
            _components.ChangeConfirmButtonText(windowData.action.actionTitle);
            _components.ShowCloseButton();
        }

        //Used in Unity Editor
        public void ConfirmButtonHandler()
        {
            _modalWindow.Close();
            _action?.DoAction();
            _action = null;
        }

        //Used in Unity Editor
        public void CloseButtonHandler()
        {
            _modalWindow?.Close();
        }
    }
}


