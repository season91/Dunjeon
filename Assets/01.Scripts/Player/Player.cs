using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어 동작 - 컨트롤러
    public PlayerController controller;
    
    // 플레이어 상태
    public PlayerStatusHandler statusHandler;
    
    private void Reset()
    {
        controller = GetComponentInChildren<PlayerController>();
        statusHandler = GetComponentInChildren<PlayerStatusHandler>();
    }

    private void Awake()
    {
        CharacterManager.Register(this);
    }

}
