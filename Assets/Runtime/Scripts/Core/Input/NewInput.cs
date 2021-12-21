// GENERATED AUTOMATICALLY FROM 'Assets/Runtime/Scripts/Core/Input/NewInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Core.Input
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
                },
                {
                    ""name"": ""Spellbook"",
                    ""type"": ""Button"",
                    ""id"": ""c6916c92-398c-4ddb-92ff-a28a88aa7cce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""e6d41680-ba18-4e48-9b41-ad3b717e7218"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""0356332d-05b2-41fa-9a7f-9c693cda36be"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spellbook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a39e2190-fa35-4606-b859-2f71083d7ec8"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
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
        },
        {
            ""name"": ""Disabled"",
            ""id"": ""71854b99-e8fd-4d39-806c-9ea4738b3d63"",
            ""actions"": [
                {
                    ""name"": ""ShowMenu"",
                    ""type"": ""Button"",
                    ""id"": ""e14b0937-1f2d-4ece-9f96-167d3624c04b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4919e406-998d-4c3f-9457-f6dff84b9fd1"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowMenu"",
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
            m_Main_Spellbook = m_Main.FindAction("Spellbook", throwIfNotFound: true);
            m_Main_Inventory = m_Main.FindAction("Inventory", throwIfNotFound: true);
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_Newaction = m_UI.FindAction("New action", throwIfNotFound: true);
            // Disabled
            m_Disabled = asset.FindActionMap("Disabled", throwIfNotFound: true);
            m_Disabled_ShowMenu = m_Disabled.FindAction("ShowMenu", throwIfNotFound: true);
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
        private readonly InputAction m_Main_Spellbook;
        private readonly InputAction m_Main_Inventory;
        public struct MainActions
        {
            private @NewInput m_Wrapper;
            public MainActions(@NewInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Click => m_Wrapper.m_Main_Click;
            public InputAction @Spellbook => m_Wrapper.m_Main_Spellbook;
            public InputAction @Inventory => m_Wrapper.m_Main_Inventory;
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
                    @Spellbook.started -= m_Wrapper.m_MainActionsCallbackInterface.OnSpellbook;
                    @Spellbook.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnSpellbook;
                    @Spellbook.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnSpellbook;
                    @Inventory.started -= m_Wrapper.m_MainActionsCallbackInterface.OnInventory;
                    @Inventory.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnInventory;
                    @Inventory.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnInventory;
                }
                m_Wrapper.m_MainActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Click.started += instance.OnClick;
                    @Click.performed += instance.OnClick;
                    @Click.canceled += instance.OnClick;
                    @Spellbook.started += instance.OnSpellbook;
                    @Spellbook.performed += instance.OnSpellbook;
                    @Spellbook.canceled += instance.OnSpellbook;
                    @Inventory.started += instance.OnInventory;
                    @Inventory.performed += instance.OnInventory;
                    @Inventory.canceled += instance.OnInventory;
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

        // Disabled
        private readonly InputActionMap m_Disabled;
        private IDisabledActions m_DisabledActionsCallbackInterface;
        private readonly InputAction m_Disabled_ShowMenu;
        public struct DisabledActions
        {
            private @NewInput m_Wrapper;
            public DisabledActions(@NewInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @ShowMenu => m_Wrapper.m_Disabled_ShowMenu;
            public InputActionMap Get() { return m_Wrapper.m_Disabled; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(DisabledActions set) { return set.Get(); }
            public void SetCallbacks(IDisabledActions instance)
            {
                if (m_Wrapper.m_DisabledActionsCallbackInterface != null)
                {
                    @ShowMenu.started -= m_Wrapper.m_DisabledActionsCallbackInterface.OnShowMenu;
                    @ShowMenu.performed -= m_Wrapper.m_DisabledActionsCallbackInterface.OnShowMenu;
                    @ShowMenu.canceled -= m_Wrapper.m_DisabledActionsCallbackInterface.OnShowMenu;
                }
                m_Wrapper.m_DisabledActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ShowMenu.started += instance.OnShowMenu;
                    @ShowMenu.performed += instance.OnShowMenu;
                    @ShowMenu.canceled += instance.OnShowMenu;
                }
            }
        }
        public DisabledActions @Disabled => new DisabledActions(this);
        public interface IMainActions
        {
            void OnClick(InputAction.CallbackContext context);
            void OnSpellbook(InputAction.CallbackContext context);
            void OnInventory(InputAction.CallbackContext context);
        }
        public interface IUIActions
        {
            void OnNewaction(InputAction.CallbackContext context);
        }
        public interface IDisabledActions
        {
            void OnShowMenu(InputAction.CallbackContext context);
        }
    }
}
