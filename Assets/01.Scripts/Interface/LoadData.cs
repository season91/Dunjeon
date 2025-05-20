using UnityEngine;

public class LoadData : MonoBehaviour
{
    // 전체 로드 실행
    private void OnEnable() => GameManager.RaiseLoad();
    private void Awake() => GameManager.RaiseAwake();
    private void Start() => GameManager.RaiseStart();
}
