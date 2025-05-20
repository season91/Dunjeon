using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어 동작 - 컨트롤러
    public PlayerController controller;

    private void Reset()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Awake()
    {
        CharacterRegistry.Register(this);
    }

}
