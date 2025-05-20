using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIStatusBar 각 상태바 공통 사용
/// </summary>
public class UIStatusBar : MonoBehaviour
{
    public float curValue;
    public float maxValue;
    public float startValue;
    public float passiveValue; // 시간이 지날수록 적용되는 값, startValue에다가 연산되는 거
    public Image uiBar;

    private void Reset()
    {
        uiBar = GetComponentsInChildren<Image>().FirstOrDefault(img => img.name == "Img_Bar");
    }

    private void Awake()
    {
        uiBar = GetComponentsInChildren<Image>().FirstOrDefault(img => img.name == "Img_Bar");
    }

    private void Start()
    {
        curValue = startValue;
    }

    private void fillAmountUpdate()
    {
        // UI 게이지 바 Update
        uiBar.fillAmount = GetPercentage();
    }
    
    // 회복
    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
        fillAmountUpdate();
    }
    
    // 회복
    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0.0f);
        fillAmountUpdate();
    }

    private float GetPercentage()
    {
        return maxValue > 0f ? curValue / maxValue : 0f;
    }
}
