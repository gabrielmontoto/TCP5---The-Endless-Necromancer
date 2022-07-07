// GENERATED AUTOMATICALLY FROM 'Assets/InputMap/Gamepad_Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Gamepad_Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Gamepad_Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Gamepad_Input"",
    ""maps"": [
        {
            ""name"": ""InputCalls"",
            ""id"": ""e3be79f5-0487-4dc0-859b-6a245940584a"",
            ""actions"": [
                {
                    ""name"": ""MoveControl"",
                    ""type"": ""PassThrough"",
                    ""id"": ""046b372d-40a1-48f0-a972-ee3971591022"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DashControl"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9c77e0f8-9b48-4b25-a2f6-bff0fd9a11a2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DashTrigger"",
                    ""type"": ""Button"",
                    ""id"": ""55ca0c1b-d8be-438a-8093-2b4d06c4c14a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""bc1bd100-ffaf-4def-9ee8-caba8c1e7e65"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NormalAttack"",
                    ""type"": ""Button"",
                    ""id"": ""b6cfc793-373c-409b-ad55-a8ccafb4f0b4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpecialAttack"",
                    ""type"": ""Button"",
                    ""id"": ""b1007e4f-5dcc-4333-b354-2ac8c178246c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""918f06f0-4fa6-4f99-a393-b5806ecea048"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""77a6cc4a-a3fa-4b30-8039-8614d5a6d44c"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""1039a511-31c4-417d-8fb5-eef16bd076c5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveControl"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""64875cba-94ce-40c8-aea1-9ec11973491a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c28becb5-aa65-4e83-b7d1-4e89ddb23fd8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ae5cea9a-1bf2-4b56-ba03-742598722ca1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c87b713f-af50-4b0f-aeab-21433e1ea80a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""12817e3c-9bf5-43d5-87bd-e36a3cd768ff"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DashControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""baa88036-bb2a-48e9-940e-d06b9ee6f990"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DashTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8124772-587e-4bd0-8916-3e3c0b686896"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DashTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e811b0e-1bac-4202-af96-7db8477a7769"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""018f84c0-2420-4c14-94cb-d755b52dbdc6"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36bd4613-36e9-40d1-b67b-c9a63e5dd644"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NormalAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5579069-33c5-421f-91bd-b2461b402db8"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NormalAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63f56eac-b5a1-496e-9c1b-f6715ea261da"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db4e6484-afb6-4ed0-b357-a365e41a3657"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6703143-f11a-46eb-95e2-b266f0d35a93"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""666d3c3f-280e-49e4-a8cc-5054d71466ac"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InputCalls
        m_InputCalls = asset.FindActionMap("InputCalls", throwIfNotFound: true);
        m_InputCalls_MoveControl = m_InputCalls.FindAction("MoveControl", throwIfNotFound: true);
        m_InputCalls_DashControl = m_InputCalls.FindAction("DashControl", throwIfNotFound: true);
        m_InputCalls_DashTrigger = m_InputCalls.FindAction("DashTrigger", throwIfNotFound: true);
        m_InputCalls_Jump = m_InputCalls.FindAction("Jump", throwIfNotFound: true);
        m_InputCalls_NormalAttack = m_InputCalls.FindAction("NormalAttack", throwIfNotFound: true);
        m_InputCalls_SpecialAttack = m_InputCalls.FindAction("SpecialAttack", throwIfNotFound: true);
        m_InputCalls_Interaction = m_InputCalls.FindAction("Interaction", throwIfNotFound: true);
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

    // InputCalls
    private readonly InputActionMap m_InputCalls;
    private IInputCallsActions m_InputCallsActionsCallbackInterface;
    private readonly InputAction m_InputCalls_MoveControl;
    private readonly InputAction m_InputCalls_DashControl;
    private readonly InputAction m_InputCalls_DashTrigger;
    private readonly InputAction m_InputCalls_Jump;
    private readonly InputAction m_InputCalls_NormalAttack;
    private readonly InputAction m_InputCalls_SpecialAttack;
    private readonly InputAction m_InputCalls_Interaction;
    public struct InputCallsActions
    {
        private @Gamepad_Input m_Wrapper;
        public InputCallsActions(@Gamepad_Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveControl => m_Wrapper.m_InputCalls_MoveControl;
        public InputAction @DashControl => m_Wrapper.m_InputCalls_DashControl;
        public InputAction @DashTrigger => m_Wrapper.m_InputCalls_DashTrigger;
        public InputAction @Jump => m_Wrapper.m_InputCalls_Jump;
        public InputAction @NormalAttack => m_Wrapper.m_InputCalls_NormalAttack;
        public InputAction @SpecialAttack => m_Wrapper.m_InputCalls_SpecialAttack;
        public InputAction @Interaction => m_Wrapper.m_InputCalls_Interaction;
        public InputActionMap Get() { return m_Wrapper.m_InputCalls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InputCallsActions set) { return set.Get(); }
        public void SetCallbacks(IInputCallsActions instance)
        {
            if (m_Wrapper.m_InputCallsActionsCallbackInterface != null)
            {
                @MoveControl.started -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnMoveControl;
                @MoveControl.performed -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnMoveControl;
                @MoveControl.canceled -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnMoveControl;
                @DashControl.started -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnDashControl;
                @DashControl.performed -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnDashControl;
                @DashControl.canceled -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnDashControl;
                @DashTrigger.started -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnDashTrigger;
                @DashTrigger.performed -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnDashTrigger;
                @DashTrigger.canceled -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnDashTrigger;
                @Jump.started -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnJump;
                @NormalAttack.started -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnNormalAttack;
                @NormalAttack.performed -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnNormalAttack;
                @NormalAttack.canceled -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnNormalAttack;
                @SpecialAttack.started -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.performed -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.canceled -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnSpecialAttack;
                @Interaction.started -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_InputCallsActionsCallbackInterface.OnInteraction;
            }
            m_Wrapper.m_InputCallsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveControl.started += instance.OnMoveControl;
                @MoveControl.performed += instance.OnMoveControl;
                @MoveControl.canceled += instance.OnMoveControl;
                @DashControl.started += instance.OnDashControl;
                @DashControl.performed += instance.OnDashControl;
                @DashControl.canceled += instance.OnDashControl;
                @DashTrigger.started += instance.OnDashTrigger;
                @DashTrigger.performed += instance.OnDashTrigger;
                @DashTrigger.canceled += instance.OnDashTrigger;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @NormalAttack.started += instance.OnNormalAttack;
                @NormalAttack.performed += instance.OnNormalAttack;
                @NormalAttack.canceled += instance.OnNormalAttack;
                @SpecialAttack.started += instance.OnSpecialAttack;
                @SpecialAttack.performed += instance.OnSpecialAttack;
                @SpecialAttack.canceled += instance.OnSpecialAttack;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
            }
        }
    }
    public InputCallsActions @InputCalls => new InputCallsActions(this);
    public interface IInputCallsActions
    {
        void OnMoveControl(InputAction.CallbackContext context);
        void OnDashControl(InputAction.CallbackContext context);
        void OnDashTrigger(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnNormalAttack(InputAction.CallbackContext context);
        void OnSpecialAttack(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
    }
}
