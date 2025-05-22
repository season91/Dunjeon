using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// [컴포넌트] Slot 1칸에 대한 클래스
/// </summary>
public class UIInventorySlot : MonoBehaviour
{
    // 그려줄 정보 icon, 개수
    public Image icon;
    public TextMeshProUGUI stackText;
    
    // 선택시 Outline 표시위한 컴포넌트
    private Button button;
    private Outline outline;
    
    // slot index, UIInventory에서 할당해줄거임
    public int index;
    
    private UIInventory inventory;

    private void Reset()
    {
        outline = GetComponent<Outline>();
        icon = GetComponentsInChildren<Image>(true).FirstOrDefault(img => img.gameObject.name == "Img_Icon");
        stackText = icon.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Init(int index, UIInventory inventory)
    {
        this.inventory = inventory;
        this.index = index;
        
        outline = GetComponent<Outline>();
        icon = GetComponentsInChildren<Image>(true).FirstOrDefault(img => img.gameObject.name == "Img_Icon");
        stackText = icon.GetComponentInChildren<TextMeshProUGUI>();
        
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClickButton);
    }

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    // slot 한칸에 대한 정보가 없다면 UI clear
    public void Clear()
    {
        icon.gameObject.SetActive(false);
        stackText.text = string.Empty;
    }
    
    // slot 한칸에 대한 정보 있다면 UI Show
    public void Show(ItemData itemData)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = itemData.icon;
        
        // 수량??
        stackText.text = string.Empty;
    }
    
    // slot 한칸 선택했을 때
    public void OnClickButton()
    {
        inventory.ShowSelectedItemUI(index);
    }
}
