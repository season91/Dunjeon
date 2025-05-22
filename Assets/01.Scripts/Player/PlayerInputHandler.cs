using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// [요청/이벤트 처리] InputSystem 이벤트 입력 수신 받아 처리할 대상에게 전달
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput  _playerInput;
    
    private InputAction move;
    private InputAction look;
    private InputAction jump;
    private InputAction interaction;
    private InputAction inventory;
    
    // 전달해줄 값
    public Vector2 MoveInput { get; private set; }
    public event Action<Vector2> OnLookInput; // 지속적으로 읽을게 아니고 값이 변경될 때만 반응하고자 델리게이트로 값 전달
    public event Action OnJumpInput; // Controller에서 +=
    public event Action OnInteractionInput; // InteractionHandler 에서 +=
    public event Action OnInventoryInput;
    
    private void Reset()
    {
        _playerInput = GetComponentInParent<PlayerInput>();
    }

    private void Awake()
    {
        _playerInput = GetComponentInParent<PlayerInput>();
        
        move = _playerInput.actions["Move"];
        look = _playerInput.actions["Look"];
        jump = _playerInput.actions["Jump"];
        interaction = _playerInput.actions["Interaction"];
        inventory = _playerInput.actions["Inventory"];

        move.Enable();
        look.Enable();
        jump.Enable();
        interaction.Enable();
        inventory.Enable();
    }

    private void Start()
    {
        move.performed += OnMovePerformed;
        move.canceled += OnMoveCanceled;
        look.performed += OnLookPerformed;
        jump.started += OnJumpStarted;
        interaction.performed += OnInteractionPerformed;
        inventory.performed += OnInventoryPerformed;
    }

    private void DisableInputAction()
    {
        move.Disable();
        look.Disable();
        jump.Disable();
        interaction.Disable();
        inventory.Disable();
    }

    private void RemoveInputAction()
    {
        move.performed -= OnMovePerformed;
        move.canceled -= OnMoveCanceled;
        look.performed -= OnLookPerformed;
        jump.started -= OnJumpStarted;
        interaction.performed -= OnInteractionPerformed;
        inventory.performed -= OnInventoryPerformed;
    }
    
    // 이동 WASD
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    // 이동 정지    
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveInput = Vector2.zero;
    }

    // 바라보는 방향
    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        OnLookInput?.Invoke(context.ReadValue<Vector2>());
    }

    // 점프
    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        OnJumpInput?.Invoke();  
    }
    
    // 상호작용 E InputAction
    private void OnInteractionPerformed(InputAction.CallbackContext context)
    {
        OnInteractionInput?.Invoke();
    }
    
    // 인벤토리 Tab
    private void OnInventoryPerformed(InputAction.CallbackContext context)
    {
        OnInventoryInput?.Invoke();
    }
}
