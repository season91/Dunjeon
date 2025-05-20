using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    // 이동
    private Vector2 curMovementInput;  // 현재 입력 값
    
    private void Reset()
    {
        _rigidbody = GetComponent<Rigidbody>(); 
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move(); // 물리연산이라 FixedUpdate
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            // 키가 떨어졌을 때 가만히 있어야 하니 처리
            curMovementInput = Vector2.zero;
        }
    }
    
    // 실제 이동 처리. 계속 처리
    private void Move()
    {
        // 방향 구하기
        // transform.forward 앞으로 뒤로 가는 밫향 
        // curMovementInput.y W, S값에 해당
        // transform.right 좌 우 방향
        // curMovementInput.x A, D 값
        Vector3 direction = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        direction *= 5; // 방향에 힘 적용
        direction.y = _rigidbody.velocity.y; // 점프시 위 아래로만 움직여야 함
        
        _rigidbody.velocity = direction;  // 연산 속도 적용해 실제 움직이게끔
    }
}
