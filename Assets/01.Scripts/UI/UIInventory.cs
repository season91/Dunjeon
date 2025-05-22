using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    // item slot 12개 정보
    [SerializeField] public Transform groupSlots;
    public UIInventorySlot[] slots;
    
    // 인벤토리 소지 정보

    // info 구성 하는 UI 정보
    // 선택한 slot 아이템 정보 표기를 위해
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemStatName;
    public TextMeshProUGUI itemStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;
    
    // 정보 저장
    private ItemData selectedItemData;
    private int selectedItemIndex;
    
    private void Reset()
    {
        groupSlots = GetComponentsInChildren<Transform>().FirstOrDefault(gameObject => gameObject.name == "Group_Slots");
        itemName = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(gameObject => gameObject.name == "Tmp_ItemName");
        itemDescription = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(gameObject => gameObject.name == "Tmp_Description");
        itemStatName = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(gameObject => gameObject.name == "Tmp_StatName");
        itemStatValue = GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(gameObject => gameObject.name == "Tmp_StatValue");
        
        useButton = GetComponentsInChildren<Button>().FirstOrDefault(gameObject => gameObject.name == "Btn_Use").gameObject;
        equipButton = GetComponentsInChildren<Button>().FirstOrDefault(gameObject => gameObject.name == "Btn_Equip").gameObject;
        unEquipButton = GetComponentsInChildren<Button>().FirstOrDefault(gameObject => gameObject.name == "Btn_UnEquip").gameObject;
        dropButton = GetComponentsInChildren<Button>().FirstOrDefault(gameObject => gameObject.name == "Btn_Drop").gameObject;
    }

    private void Start()
    {
        // 인벤토리창 처음에 꺼주기
        gameObject.SetActive(false); 
        
        // 반복문으로 각 슬롯 생성
        slots = new UIInventorySlot[groupSlots.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = groupSlots.GetChild(i).GetComponent<UIInventorySlot>();
            slots[i].Init(i, this);
        }
        
        UpdateInventoryUI(new List<ItemData>()); // 처음에 인벤토리 목록 전부 비우기
        ClearSelectedItemUI(); // 아이템 설명 창 내용 비우기
    }
    
    // 인벤토리 창 정보 update시 새로고침
    public void UpdateInventoryUI(List<ItemData> items)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].Show(items[i]);
            }
            else
            {
                slots[i].Clear();
            }
        }
    }
    
    // 인벤토리 창 open/close 
    public void Toggle()
    {
        if (IsOpen())
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
    
    // 켜졌는지 확인
    public bool IsOpen()
    {
        return this.gameObject.activeInHierarchy;
    }

    #region 선택한 아이템 정보 관련

    private void ClearSelectedItemUI()
    {
        itemName.text = string.Empty; 
        itemDescription.text = string.Empty;
        itemStatName.text = string.Empty;
        itemStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }
    
    // 선택한 아이템 정보 그려주기
    public void ShowSelectedItemUI(int index)
    {
        List<ItemData> items = InventoryManager.inventoryItems;
        // 범위 체크
        if (index < 0 || index >= items.Count || items[index] == null)
            return;
        
        // 선택한 아이템에 대한 정보 저장
        selectedItemData = items[index];
        selectedItemIndex = index;

        itemName.text = selectedItemData.displayName;
        itemDescription.text = selectedItemData.description;

        itemStatName.text = string.Empty;
        itemStatValue.text = string.Empty;
        
        for (int i = 0; i < selectedItemData.consumables.Length; i++)
        {
            itemStatName.text = selectedItemData.consumables[i].type.ToString()+"\n";
            itemStatValue.text = selectedItemData.consumables[i].value.ToString()+"\n";
            
        }
        
        useButton.SetActive(selectedItemData.type == ItemType.Consumable);
        dropButton.SetActive(true);
    }

    #endregion
    
}
