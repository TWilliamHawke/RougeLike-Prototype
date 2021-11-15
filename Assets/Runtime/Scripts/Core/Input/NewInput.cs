// GENERATED AUTOMATICALLY FROM 'Assets/Runtime/Scripts/Core/Input/NewInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Core
{
    public class @NewInput : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @NewInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""NewInput"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""80eeb649-f17f-4181-9b27-e0cecd859354"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""e1b7f3ed-a348-4a50-9e38-413a94b9fef5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5d9df1a6-2742-4a92-b458-44299c77b31d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""eed3c426-dde9-4260-8009-e2bd8b076bef"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""413e866a-ab0f-4bc2-870c-8aa731d34237"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""66771ce9-e08a-4163-a13a-064b25dbc529"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Main
            m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
            m_Main_Click = m_Main.FindAction("Click", throwIfNotFound: true);
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_Newaction = m_UI.FindAction("New action", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Main
        private readonly InputActionMap m_Main;
        private IMainActions m_MainActionsCallbackInterface;
        private readonly InputAction m_Main_Click;
        public struct MainActions
        {
            private @NewInput m_Wrapper;
            public MainActions(@NewInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Click => m_Wrapper.m_Main_Click;
            public InputActionMap Get() { return m_Wrapper.m_Main; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
            public void SetCallbacks(IMainActions instance)
            {
                if (m_Wrapper.m_MainActionsCallbackInterface != null)
                {
                    @Click.started -= m_Wrapper.m_MainActionsCallbackInterface.OnClick;
                    @Click.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnClick;
                    @Click.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnClick;
                }
                m_Wrapper.m_MainActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Click.started += instance.OnClick;
                    @Click.performed += instance.OnClick;
                    @Click.canceled += instance.OnClick;
                }
            }
        }
        public MainActions @Main => new MainActions(this);

        // UI
        private readonly InputActionMap m_UI;
        private IUIActions m_UIActionsCallbackInterface;
        private readonly InputAction m_UI_Newaction;
        public struct UIActions
        {
            private @NewInput m_Wrapper;
            public UIActions(@NewInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Newaction => m_Wrapper.m_UI_Newaction;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void SetCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterface != null)
                {
                    @Newaction.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                    @Newaction.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                    @Newaction.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                }
                m_Wrapper.m_UIActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Newaction.started += instance.OnNewaction;
                    @Newaction.performed += instance.OnNewaction;
                    @Newaction.canceled += instance.OnNewaction;
                }
            }
        }
        public UIActions @UI => new UIActions(this);
        public interface IMainActions
        {
            void OnClick(InputAction.CallbackContext context);
        }
        public interface IUIActions
        {
            void OnNewaction(InputAction.CallbackContext context);
        }
    }
}
