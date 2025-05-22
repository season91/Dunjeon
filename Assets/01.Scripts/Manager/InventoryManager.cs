using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// [전역] 인벤토리 데이터만 관리
/// </summary>
public static class InventoryManager
{
    private static int MaxSlotCount = 12;
    public static List<ItemData> inventoryItems = new List<ItemData>();

    public static void AddItem(ItemData item)
    {
        if (inventoryItems.Count >= MaxSlotCount)
        {
            Debug.Log("12개 꽉 참");
            return;
        }
        
        inventoryItems.Add(item);
        UIManager.Instance.InventoryRefresh(inventoryItems);
    }
    
    // 아이템 버리기
    public static void RemoveItem(ItemData item)
    {
        inventoryItems.Remove(item);
    }

}
