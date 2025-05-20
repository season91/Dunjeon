using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// UIStatusBar의 상태바 묶음
/// </summary>
public class UIStatusGroup : MonoBehaviour
{
    [SerializeField] private UIStatusBar hpBar;
    [SerializeField] private UIStatusBar hungerBar;
    [SerializeField] private UIStatusBar staminaBar;

    private void Reset()
    {
        hpBar = GetComponentsInChildren<UIStatusBar>().FirstOrDefault(bar => bar.name == "Group_StatusBar_Health");
        hungerBar = GetComponentsInChildren<UIStatusBar>().FirstOrDefault(bar => bar.name == "Group_StatusBar_Hunger");
        staminaBar = GetComponentsInChildren<UIStatusBar>().FirstOrDefault(bar => bar.name == "Group_StatusBar_Stamina");
    }

    public void AddHp(float value) => hpBar.Add(value);
    public void AddHunger(float value) => hungerBar.Add(value);
    public void AddStamina(float value) => staminaBar.Add(value);

    public void SubtractHp(float value) => hpBar.Subtract(value);
    public void SubtractHunger(float value) => hungerBar.Subtract(value);
    public void SubtractStamina(float value) => staminaBar.Subtract(value);
}
