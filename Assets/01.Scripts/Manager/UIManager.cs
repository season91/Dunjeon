using System;
using UnityEngine;
/// <summary>
/// UIManager 싱글톤
/// </summary>
public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;

    UIStatusGroup statusGroup;
    
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    private void Reset()
    {
        statusGroup = GetComponentInChildren<UIStatusGroup>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        statusGroup = GetComponentInChildren<UIStatusGroup>();
    }

    public void SetHp(float value) => statusGroup.AddHp(value);

    // 아이템 타입에 따라 수정 확장 필요
    public void SetHunger(float value) => statusGroup.SubtractHunger(value);
}