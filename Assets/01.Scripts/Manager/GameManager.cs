using System;
using UnityEngine;

public static class GameManager 
{
    private static event Action OnLoad;
    private static event Action OnAwake;
    private static event Action OnStart;
    
    // 호출
    // 이걸 하는 이유는? 유니티 생명주기 대상이 아니거나 생명주기로 컨트롤이 어려운 경우
    public static void RaiseLoad()
    {
        if (OnLoad == null)
        {
            Debug.Log("OnLoad 등록된 것 없음");
            return;
        }
        OnLoad?.Invoke();
        OnLoad = null;
    }

    public static void RaiseAwake()
    {
        if (OnAwake == null)
        {
            Debug.Log("OnAwake 등록된 것 없음");
            return;
        }
        OnAwake?.Invoke();
        OnAwake = null;
    }

    public static void RaiseStart()
    {
        if (OnStart == null)
        {
            Debug.Log("OnStart 등록된 것 없음");
            return;
        }
        OnStart?.Invoke();
        OnStart = null;
    }

    // 델리게이트에 함수 등록
    public static void SetLoadAction(Action action)
    {
        OnLoad += action;
    }
    
    public static void SetAwakeAction(Action action)
    {
        OnAwake += action;
    }

    public static void SetStartAction(Action action)
    {
        OnStart += action;
    }
}
