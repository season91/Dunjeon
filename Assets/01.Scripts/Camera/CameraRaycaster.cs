using System;
using UnityEngine;

/// <summary>
/// [컴포넌트] CameraContainer 기준으로 Ray. Ray는 감지만 
/// </summary>
public class CameraRaycaster : MonoBehaviour
{
    // 카메라 기준
    private Camera _camera; // ray 방향
    
    // 상호작용 시간 체크 - Update에서 매프레임 호출 방지를 위해 마지막 체크 시간 기준으로 검증
    public float checkRate = 0.05f; // 상호작용 오브젝트 체크 시간. 카메라 기준으로 Ray 쏠건데, 주기 얼마로 할건지
    private float lastCheckTime; // 마지막 상호작용 체크 시간
    
    // 레이캐스트에서 사용할 정보
    public float maxCheckDistance; // 최대 체크 거리 얼마나 멀리 있는거 체크할지
    public LayerMask layerMask; // 어떤 레이어 추출한건지 - Player 제외한 모두로 설정함

    // 상호 작용 아이템 정보
    private GameObject curInpectGameObject; // 현재 상호작용 아이템 설명만
    private GameObject curInteractGameObject; // 현재 상호작용 아이템 E 포함

    // 상호 작용 델리게이트
    public event Action<IInspectable> OnInteractChanged;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate) // 매프레임 호출 방지를 위해 마지막 체크 시간 기준으로 검증
        {
            lastCheckTime = Time.time;
            CheckForInteractable();
        }
    }


    private void CheckForInteractable()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction * maxCheckDistance, Color.red);

        if (!Physics.Raycast(ray, out RaycastHit hit, maxCheckDistance, layerMask))
        {
            curInteractGameObject = null;
            OnInteractChanged?.Invoke(null);
            return;
        }
        
        DrawCircle(hit.point, 0.15f, Color.blue);
        
        if(hit.collider.gameObject != curInteractGameObject && hit.collider.GetComponent<IInspectable>() is IInspectable inspectable)
        {
            curInteractGameObject = hit.collider.gameObject;
            OnInteractChanged?.Invoke(inspectable);
        }
    }
    
    private void DrawCircle(Vector3 center, float radius, Color color, int segments = 20) {
        float angleStep = 360f / segments;
        Vector3 lastPoint = center + new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)) * radius;

        for (int i = 1; i <= segments; i++) {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

            Debug.DrawLine(lastPoint, nextPoint, color);
            lastPoint = nextPoint;
        }
    }
}
