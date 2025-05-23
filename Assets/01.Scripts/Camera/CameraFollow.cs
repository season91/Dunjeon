using System;
using UnityEngine;
/// <summary>
/// Player-CameraFollowTarget 따라가기
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Transform target; // 런타임에 동적으로 설정할 거라 여기선 일단 냅둠?
    public Vector3 cameraOffset = new Vector3(0, 2.5f, 0);
    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 offsetPos = target.position + cameraOffset - target.forward * 4f;
        transform.position = offsetPos;
        transform.LookAt(target.position + cameraOffset * 0.5f); // 약간 아래로 시선
    }
}
