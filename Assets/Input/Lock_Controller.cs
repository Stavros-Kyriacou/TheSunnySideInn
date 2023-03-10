//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Input/Lock_Controller.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Lock_Controller : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Lock_Controller()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Lock_Controller"",
    ""maps"": [
        {
            ""name"": ""Player_Map"",
            ""id"": ""ece6dcf6-a8ff-46e0-b653-4da9deb6e8cb"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""c40db720-c750-4600-96a8-0776d312bde2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""881b78ab-a061-4d2b-9109-6f11623163f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""1ccbd292-dac4-4845-a168-8c09ee02392a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""1a2e5376-ddec-43e4-be23-23eab4230d57"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Exit"",
                    ""type"": ""Button"",
                    ""id"": ""2766809e-8340-4c95-9d59-77749712df71"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""40ed39f2-4635-44ea-aa4d-fbce161061a9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7522f248-ddbd-4460-9e0c-ae0404b721de"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6bb4caa3-1be7-45b4-a595-451483ba5205"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""878a9dfc-dad0-4973-9d5a-76263eeb79cb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6ba2b43-cc37-4933-bde9-c769a3277cb2"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player_Map
        m_Player_Map = asset.FindActionMap("Player_Map", throwIfNotFound: true);
        m_Player_Map_Up = m_Player_Map.FindAction("Up", throwIfNotFound: true);
        m_Player_Map_Down = m_Player_Map.FindAction("Down", throwIfNotFound: true);
        m_Player_Map_Left = m_Player_Map.FindAction("Left", throwIfNotFound: true);
        m_Player_Map_Right = m_Player_Map.FindAction("Right", throwIfNotFound: true);
        m_Player_Map_Exit = m_Player_Map.FindAction("Exit", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player_Map
    private readonly InputActionMap m_Player_Map;
    private IPlayer_MapActions m_Player_MapActionsCallbackInterface;
    private readonly InputAction m_Player_Map_Up;
    private readonly InputAction m_Player_Map_Down;
    private readonly InputAction m_Player_Map_Left;
    private readonly InputAction m_Player_Map_Right;
    private readonly InputAction m_Player_Map_Exit;
    public struct Player_MapActions
    {
        private @Lock_Controller m_Wrapper;
        public Player_MapActions(@Lock_Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @Up => m_Wrapper.m_Player_Map_Up;
        public InputAction @Down => m_Wrapper.m_Player_Map_Down;
        public InputAction @Left => m_Wrapper.m_Player_Map_Left;
        public InputAction @Right => m_Wrapper.m_Player_Map_Right;
        public InputAction @Exit => m_Wrapper.m_Player_Map_Exit;
        public InputActionMap Get() { return m_Wrapper.m_Player_Map; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_MapActions set) { return set.Get(); }
        public void SetCallbacks(IPlayer_MapActions instance)
        {
            if (m_Wrapper.m_Player_MapActionsCallbackInterface != null)
            {
                @Up.started -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnDown;
                @Left.started -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnRight;
                @Exit.started -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnExit;
                @Exit.performed -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnExit;
                @Exit.canceled -= m_Wrapper.m_Player_MapActionsCallbackInterface.OnExit;
            }
            m_Wrapper.m_Player_MapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Exit.started += instance.OnExit;
                @Exit.performed += instance.OnExit;
                @Exit.canceled += instance.OnExit;
            }
        }
    }
    public Player_MapActions @Player_Map => new Player_MapActions(this);
    public interface IPlayer_MapActions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnExit(InputAction.CallbackContext context);
    }
}
