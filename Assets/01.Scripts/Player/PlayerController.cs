using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 움직임 처리
/// </summary>
public class PlayerController : MonoBehaviour
{
    // 이동에 필요한 변수
    [SerializeField] private PlayerInputHandler playerInput;
    [SerializeField] private Rigidbody playerRigidbody;
    private Transform playerTransform;
    public float moveSpeed = 5f;
    
    // 점프에 필요한 변수
    public LayerMask groundLayerMask;
    public float jumpPower;
    
    // 회전에 필요한 변수
    public Transform cameraContainer;
    public float minXLook = -65;  // 최소 시야각
    public float maxXLook = 65;  // 최대 시야각
    public float lookSensitivity = 0.1f; // 회전 민감도
    private float camCurXRot; // 마우스 델타값 적용
    
    
    private void Reset()
    {
        playerRigidbody = GetComponentInParent<Rigidbody>(); 
        playerInput = transform.parent.GetComponentInChildren<PlayerInputHandler>();
    }

    private void Awake()
    {
        playerRigidbody = GetComponentInParent<Rigidbody>();
        playerInput = transform.parent.GetComponentInChildren<PlayerInputHandler>();
        playerTransform = transform.parent;
    }
    
    private void OnEnable()
    {
        playerInput.OnLookInput += Look;
        playerInput.OnJumpInput += Jump;
    }

    private void OnDisable()
    {
        playerInput.OnLookInput -= Look;
        playerInput.OnJumpInput -= Jump;
    }


    private void FixedUpdate()
    {
        Move(); // 물리연산이라 FixedUpdate
    }
    
    // 실제 이동 처리. 계속 처리
    private void Move()
    {
        Vector3 direction = playerTransform.forward * playerInput.MoveInput.y + playerTransform.right * playerInput.MoveInput.x;
        direction *= moveSpeed; // 방향에 힘 적용
        direction.y = playerRigidbody.velocity.y; // 점프시 위 아래로만 움직여야 함
        playerRigidbody.velocity = direction;  // 연산 속도 적용해 실제 움직이게끔
    }

    private void Look(Vector2 mouseDelta)
    {
        // 마우스 움직임의 변화량(mouseDelta)중 y(위 아래)값에 민감도를 곱한다.
        // 카메라가 위 아래로 회전하려면 rotation의 x 값에 넣어준다. -> 실습으로 확인
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        
        // 마우스 움직임의 변화량(mouseDelta)중 x(좌우)값에 민감도를 곱한다.
        // 카메라가 좌우로 회전하려면 rotation의 y 값에 넣어준다. -> 실습으로 확인
        // 좌우 회전은 플레이어(transform)를 회전시켜준다.
        // Why? 회전시킨 방향을 기준으로 앞뒤좌우 움직여야하니까.
        // 캐릭터 좌표 mouseDelta.x * lookSensitivity
        playerTransform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); // 위,아래 캐릭터 각도 회전
    }

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

        Ray[] rays = new Ray[4]
        {
            new Ray(origin + (playerTransform.forward * offset) + Vector3.up * -rayHeightOffset, Vector3.down),
            new Ray(origin + (-playerTransform.forward * offset) + Vector3.up * -rayHeightOffset, Vector3.down),
            new Ray(origin + (playerTransform.right * offset) + Vector3.up * -rayHeightOffset, Vector3.down),
            new Ray(origin + (-playerTransform.right * offset) + Vector3.up * -rayHeightOffset, Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            return true;
        }

        return false;
    }
    
}
