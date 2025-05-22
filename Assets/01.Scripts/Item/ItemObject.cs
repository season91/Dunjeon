using UnityEngine;

/// <summary>
/// [컴포넌트] 각 Item의 Prefab
/// </summary>

// 상호작용 시스템은 주로 인터페이스 기반으로 출력, E키 상호작용 한다고 함
public interface IInteractable
{
    public string GetPromptText(); // UI에 표시할 설명
    public void OnInteract(); // E 키 상호작용
}
public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    private void Reset()
    {
        // csv import 선행 작업 필요
        data = Resources.Load<ItemData>($"Item/Data/{this.name}");
    }

    public string GetPromptText()
    {
        string str = $"\n{data.displayName}\n{data.description}";
        return str;
    }

    public void OnInteract()
    {
        // E키로 상호작용이 되었을 때
        // 아이템 습득이 되고, 현재 object는 파괴
        InventoryManager.AddItem(data);
        Destroy(gameObject);
    }
}
