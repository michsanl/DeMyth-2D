//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/@Productions/Input System/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""562f6b15-b7ec-4807-a63f-73268de1e5a9"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""fe2db2eb-efce-4d14-bd18-14a9ee52ff92"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Senter"",
                    ""type"": ""Button"",
                    ""id"": ""1745a3d4-015c-4238-9e59-799b80bf601d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HealthPotion"",
                    ""type"": ""Button"",
                    ""id"": ""5fdc3432-b974-4fb4-988a-6f86d2874af5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""9456a498-554b-4a1b-8fc6-6fcbab6f8107"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""9c0f0cc6-21ab-4d6d-bb59-0d5ced0cb9a4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8a7e4d32-a05a-452a-834a-704903ac1909"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""03794ae5-e187-446d-a740-4e4dc102ae75"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6300ab20-2d49-4f71-a50c-8feaa7c990a1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d4700df1-2d4c-4260-abb7-c989296280ff"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""52fa522e-23c8-4a02-9a86-9787a75ee77b"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Senter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e86442c6-b688-444c-8a6e-860db29151c9"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HealthPotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""541228ee-f741-4108-93d9-03b4c29c32bd"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Pause"",
            ""id"": ""2c404f06-9c06-48fd-b0ec-12d8a715061f"",
            ""actions"": [
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""57989e4d-0fbf-447b-a4ce-0585b8994009"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8560a241-7266-4c90-8653-94c6432e597d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Restart"",
            ""id"": ""4857d55b-1f83-4513-8f60-aff9d4649848"",
            ""actions"": [
                {
                    ""name"": ""RestartLevel"",
                    ""type"": ""Button"",
                    ""id"": ""a3ac8850-c8ff-4c2c-8611-e726f400a0d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9e6b8c61-ab67-4e68-bfeb-30d3568f5dca"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RestartLevel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Senter = m_Player.FindAction("Senter", throwIfNotFound: true);
        m_Player_HealthPotion = m_Player.FindAction("HealthPotion", throwIfNotFound: true);
        m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
        // Pause
        m_Pause = asset.FindActionMap("Pause", throwIfNotFound: true);
        m_Pause_Escape = m_Pause.FindAction("Escape", throwIfNotFound: true);
        // Restart
        m_Restart = asset.FindActionMap("Restart", throwIfNotFound: true);
        m_Restart_RestartLevel = m_Restart.FindAction("RestartLevel", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Senter;
    private readonly InputAction m_Player_HealthPotion;
    private readonly InputAction m_Player_Pause;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Senter => m_Wrapper.m_Player_Senter;
        public InputAction @HealthPotion => m_Wrapper.m_Player_HealthPotion;
        public InputAction @Pause => m_Wrapper.m_Player_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Senter.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSenter;
                @Senter.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSenter;
                @Senter.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSenter;
                @HealthPotion.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHealthPotion;
                @HealthPotion.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHealthPotion;
                @HealthPotion.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHealthPotion;
                @Pause.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Senter.started += instance.OnSenter;
                @Senter.performed += instance.OnSenter;
                @Senter.canceled += instance.OnSenter;
                @HealthPotion.started += instance.OnHealthPotion;
                @HealthPotion.performed += instance.OnHealthPotion;
                @HealthPotion.canceled += instance.OnHealthPotion;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Pause
    private readonly InputActionMap m_Pause;
    private IPauseActions m_PauseActionsCallbackInterface;
    private readonly InputAction m_Pause_Escape;
    public struct PauseActions
    {
        private @PlayerInputActions m_Wrapper;
        public PauseActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Escape => m_Wrapper.m_Pause_Escape;
        public InputActionMap Get() { return m_Wrapper.m_Pause; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseActions set) { return set.Get(); }
        public void SetCallbacks(IPauseActions instance)
        {
            if (m_Wrapper.m_PauseActionsCallbackInterface != null)
            {
                @Escape.started -= m_Wrapper.m_PauseActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_PauseActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_PauseActionsCallbackInterface.OnEscape;
            }
            m_Wrapper.m_PauseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
            }
        }
    }
    public PauseActions @Pause => new PauseActions(this);

    // Restart
    private readonly InputActionMap m_Restart;
    private IRestartActions m_RestartActionsCallbackInterface;
    private readonly InputAction m_Restart_RestartLevel;
    public struct RestartActions
    {
        private @PlayerInputActions m_Wrapper;
        public RestartActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @RestartLevel => m_Wrapper.m_Restart_RestartLevel;
        public InputActionMap Get() { return m_Wrapper.m_Restart; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RestartActions set) { return set.Get(); }
        public void SetCallbacks(IRestartActions instance)
        {
            if (m_Wrapper.m_RestartActionsCallbackInterface != null)
            {
                @RestartLevel.started -= m_Wrapper.m_RestartActionsCallbackInterface.OnRestartLevel;
                @RestartLevel.performed -= m_Wrapper.m_RestartActionsCallbackInterface.OnRestartLevel;
                @RestartLevel.canceled -= m_Wrapper.m_RestartActionsCallbackInterface.OnRestartLevel;
            }
            m_Wrapper.m_RestartActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RestartLevel.started += instance.OnRestartLevel;
                @RestartLevel.performed += instance.OnRestartLevel;
                @RestartLevel.canceled += instance.OnRestartLevel;
            }
        }
    }
    public RestartActions @Restart => new RestartActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSenter(InputAction.CallbackContext context);
        void OnHealthPotion(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IPauseActions
    {
        void OnEscape(InputAction.CallbackContext context);
    }
    public interface IRestartActions
    {
        void OnRestartLevel(InputAction.CallbackContext context);
    }
}
