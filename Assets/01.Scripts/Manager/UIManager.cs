using System;
using UnityEngine;
/// <summary>
/// UIManager 싱글톤
/// </summary>
public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;

    private UIStatusGroup statusGroup;
    private UIPrompt prompt;

    [SerializeField] private RectTransform aim;
    
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
        prompt = GetComponentInChildren<UIPrompt>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        statusGroup = GetComponentInChildren<UIStatusGroup>();
        prompt = GetComponentInChildren<UIPrompt>();
    }

    public void SetHp(float value) => statusGroup.AddHp(value);

    // 아이템 타입에 따라 수정 확장 필요
    public void SetHunger(float value) => statusGroup.SubtractHunger(value);
    
    // 아이템 출력
    public void ShowDescriptionPrompt(string description) => prompt.ShowDescriptionPrompt(description);
    public void HideDescriptionPrompt() => prompt.HideDescriptionPrompt();
    
    public void SetAimPosition(Vector3 screenPos) {
        aim.position = screenPos;
    }
}