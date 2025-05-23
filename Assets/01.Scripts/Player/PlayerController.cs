using System;
using Fusion;
using UnityEngine;

/// <summary>
/// [주도적으로 처리] 직접 움직임 처리
/// </summary>
public class PlayerController : NetworkBehaviour, IJumpable
{
    // 이동에 필요한 변수
    [SerializeField] private PlayerInputHandler playerInput;
    [SerializeField] private Rigidbody playerRigidbody;
    private Transform playerTransform;
    public float moveSpeed = 5f;
    
    // 점프에 필요한 변수
    public float jumpPower;
    
    // 회전에 필요한 변수
    public Transform cameraFollowTarget;    // Main Camera가 위치 따라가는 빈 오브젝트
    public float minXLook = -65;  // 최소 시야각
    public float maxXLook = 65;  // 최대 시야각
    [Range(0.1f, 10f)]
    public float lookSensitivity = 0.1f; // 회전 민감도
    private float camCurXRot; // 마우스 델타값 적용
    public bool canLook = true;
    
    private void Reset()
    {
        cameraFollowTarget = transform.parent.GetComponentInChildren<CameraRaycaster>().transform;
        playerRigidbody = GetComponentInParent<Rigidbody>(); 
        playerInput = transform.parent.GetComponentInChildren<PlayerInputHandler>();
    }
    
    // Fusion 연결
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            // 카메라에 따라가는 대상 대신 초기화
            Camera.main.GetComponent<CameraFollow>().target = cameraFollowTarget;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            cameraFollowTarget = transform.parent.GetComponentInChildren<CameraRaycaster>().transform;
            playerRigidbody = GetComponentInParent<Rigidbody>();
            playerInput = transform.parent.GetComponentInChildren<PlayerInputHandler>();
            playerTransform = transform.parent;
            
            playerInput.OnLookInput += Look;
            playerInput.OnJumpInput += Jump;
            playerInput.OnInventoryInput += ToggleInventory;
        }
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        playerInput.OnLookInput -= Look;
        playerInput.OnJumpInput -= Jump;
        playerInput.OnInventoryInput -= ToggleInventory;
    }

    #region local 환경
    
    // private void Awake()
    // {
    //     cameraFollowTarget = transform.parent.GetComponentInChildren<CameraRaycaster>().transform;
    //     playerRigidbody = GetComponentInParent<Rigidbody>();
    //     playerInput = transform.parent.GetComponentInChildren<PlayerInputHandler>();
    //     playerTransform = transform.root;
    //     cameraTransform = Camera.main.transform;
    // }
    //
    // private void OnEnable()
    // {
    //     playerInput.OnLookInput += Look;
    //     playerInput.OnJumpInput += Jump;
    //     playerInput.OnInventoryInput += ToggleInventory;
    // }

    // private void OnDisable()
    // {
    //     playerInput.OnLookInput -= Look;
    //     playerInput.OnJumpInput -= Jump;
    //     playerInput.OnInventoryInput -= ToggleInventory;
    // }
    // Player간 입력 분리
    
    // private void FixedUpdate()
    // {
    //     Move(); // 물리연산이라 FixedUpdate
    // }
    #endregion
    private void Update()
    {
        if (Object.HasInputAuthority) return; // 본인 플레이어만 조작
    }

    private void LateUpdate()
    {  // 카메라 타겟이 부모(Player)의 회전에 영향을 받아 돌아가는 것 막기
        
        if (!Object.HasInputAuthority) return;
        Vector3 currentRot = cameraFollowTarget.localEulerAngles;
        cameraFollowTarget.localEulerAngles = new Vector3(currentRot.x, 0f, 0f);
        
    }
    public override void FixedUpdateNetwork()
    {
        if (!Object.HasInputAuthority) return; // 본인 플레이어만 조작
        FusionMove();
    }

    #region 움직임 관련

    private void FusionMove()
    {
        Vector3 direction = playerTransform.forward * playerInput.MoveInput.y + playerTransform.right * playerInput.MoveInput.x;
        direction *= moveSpeed; // 방향에 힘 적용
        direction.y = playerRigidbody.velocity.y; // 점프시 위 아래로만 움직여야 함
        playerRigidbody.velocity = direction;  
    }
    
    // 실제 이동 처리. 계속 처리
    private void LocalMove()
    {
        Vector3 direction = playerTransform.forward * playerInput.MoveInput.y + playerTransform.right * playerInput.MoveInput.x;
        direction *= moveSpeed; // 방향에 힘 적용
        direction.y = playerRigidbody.velocity.y; // 점프시 위 아래로만 움직여야 함
        playerRigidbody.velocity = direction;  // 연산 속도 적용해 실제 움직이게끔
    }
    
    // 실제 회전 처리 마우스 방향으로 -> 3인칭으로 수정
    private void Look(Vector2 mouseDelta)
    {
        if (UIManager.Instance.IsInventoryOpen()) return;
        if (mouseDelta.sqrMagnitude < 0.0001f) return;
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        // 좌우 회전 (캐릭터 전체)
        playerTransform.Rotate(Vector3.up * mouseDelta.x * lookSensitivity);

        // 상하 회전만 cameraFollowTarget에 적용
        Vector3 newAngles = new Vector3(-camCurXRot, 0f, 0f); // Y, Z = 0 고정
        cameraFollowTarget.localEulerAngles = newAngles;
    }
    
    private void LocalLook(Vector2 mouseDelta)
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        // cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        // playerTransform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    // 실제 점프 처리
    private void Jump()
    {
        if (IsGrounded())
        {
            playerRigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }
    
    private bool IsGrounded()
    {
        Vector3 origin = playerTransform.position;
        float offset = 0.2f;
        float rayHeightOffset = 0.1f; // 발보다 살짝 위에서 시작
        float rayDistance = 0.25f;    // 레이 길이
        bool isGrounded = false;

        Ray[] rays = new Ray[4]
        {
            new Ray(origin + (playerTransform.forward * offset) + Vector3.up * -rayHeightOffset, Vector3.down),
            new Ray(origin + (-playerTransform.forward * offset) + Vector3.up * -rayHeightOffset, Vector3.down),
            new Ray(origin + (playerTransform.right * offset) + Vector3.up * -rayHeightOffset, Vector3.down),
            new Ray(origin + (-playerTransform.right * offset) + Vector3.up * -rayHeightOffset, Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            Debug.DrawRay(rays[i].origin, rays[i].direction * rayDistance, Color.green); // Scene에서 시각화
            isGrounded =  true;
            break;
        }

        return isGrounded;
    }

    // 점프대 실제 점프 처리
    public void Launch(float force)
    {
        playerRigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
    #endregion

    #region 인벤토리 관련

    // OnInventoryInput에 인벤토리 open/close 연결해주어야함
    // UI는 그리는 역할만 해야하기 때문
    public void ToggleInventory()
    {
        if (!UIManager.Instance.IsInventoryOpen())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerInput.DisableInputAction();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerInput.EnableInputAction();
        }
        
        UIManager.Instance.ToggleInventory();
    }
    
    #endregion
}
