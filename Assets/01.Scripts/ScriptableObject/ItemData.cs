using System;
using UnityEngine;
/// <summary>
/// [데이터] 아이템 데이터 틀
/// </summary>

// 아이템 타입
public enum ItemType
{
    Resource, // 단순 자원
    Equipable, // 장착이 가능한 
    Consumable // 소비 하는
}

// 소비 타입
public enum ConsumableType
{
    Hunger,
    Health
}

// 소비 타입 아이템인 경우 회복량
[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value; // 얼만큼 회복 시켜줄 지
}

// 빠르게 만들 수 있게 에디터 메뉴창에 추가
[CreateAssetMenu(fileName ="Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    // 아이템에 들어갈 정보들
    public string itemCode;
    
    [Header("아이템에 들어갈 정보 Info")] 
    public string displayName; // 아이템 이름
    public string description; // 아이템 설명
    public ItemType type; // 회복, 공격, 채집 등으로 인한 각 아이템 타입
    public Sprite icon;
    public GameObject prefab; // 프리팹 정보

    // 여러 개 가지는 것에 대한 표시
    [Header("여러 개 가지는 것에 대한 표시 Stacking")]
    public bool isStackable; // 여러개 가질 수 있는 아이템인지 구분
    public int maxStack; // 얼마나 

    // 아이템 먹었을 때 회복
    [Header("소비 타입 아이템 회복량 Consumable")]
    public ItemDataConsumable[] consumables;
}
