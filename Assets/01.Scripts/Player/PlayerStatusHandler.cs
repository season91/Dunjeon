using System;
using UnityEngine;
/// <summary>
/// [요청/이벤트 처리] 상태 변경 요청 처리
/// </summary>
public class PlayerStatusHandler : MonoBehaviour
{
    private void Update()
    {
        // 테스트용 코드
        UIManager.Instance.SetHunger(10* Time.deltaTime);
    }
}
