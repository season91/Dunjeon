using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UIManager 싱글톤
/// </summary>
public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;

    // 상태
    private UIStatusGroup statusGroup;
    
    // 상호작용 출력
    private UIPrompt prompt;
    
    // 인벤토리
    private UIInventory inventory;

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
        inventory =  GetComponentInChildren<UIInventory>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        statusGroup = GetComponentInChildren<UIStatusGroup>();
        prompt = GetComponentInChildren<UIPrompt>();
        inventory =  GetComponentInChildren<UIInventory>();
    }

    public void AddHealth(float value) => statusGroup.AddHp(value);
    public void SubstractHealth(float value) => statusGroup.SubtractHp(value);

    // 아이템 타입에 따라 수정 확장 필요
    public void SubstractHunger(float value) => statusGroup.SubtractHunger(value);
    public void AddHunger(float value) => statusGroup.AddHunger(value);
    
    // 아이템 출력
    public void ShowDescriptionPrompt(string description) => prompt.ShowDescriptionPrompt(description);
    public void HideDescriptionPrompt() => prompt.HideDescriptionPrompt();
    
    // 인벤토리 열기
    public void ToggleInventory() => inventory.Toggle();
    
    // 인벤토리 UI refresh
    public void InventoryRefresh(List<ItemData> inventoryItems) => inventory.UpdateInventoryUI(inventoryItems);
}