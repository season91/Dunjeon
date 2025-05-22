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
    
    // 아이템 사용
    public static void UseItem(ItemData item)
    {
        for (int i = 0; i < item.consumables.Length; i++)
        {
            switch (item.consumables[i].type)
            {
                case ConsumableType.Health:
                    CharacterManager.Player.statusHandler.Heal(item.consumables[i].value);
                    break;
                case ConsumableType.Hunger:
                    CharacterManager.Player.statusHandler.Hunger(item.consumables[i].value);
                    break;
            }
        }
    }
    
    // 아이템 장착
    
}
