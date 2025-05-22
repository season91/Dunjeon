#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// [공통] ItemData csv import. 에디터 에서 scriptable화 하는 것
/// 에디터 상단 Tools -> CSV Import -> Import ItemData 클릭후 itemdata.csv 선택
/// Assets/Data/Items 내에 생성될 것
/// </summary>
public class CSVImporter 
{
    #region Item 생성
    [MenuItem("Tools/CSV Import/Import ItemData")]
    
    public static void ImportFromItemDataCSV() {
        string path = EditorUtility.OpenFilePanel("Select CSV", "", "csv");
        if (string.IsNullOrEmpty(path)) return;

        string[] lines = File.ReadAllLines(path);
        if (lines.Length <= 1) {
            Debug.LogWarning("CSV has no data.");
            return;
        }

        string targetFolder = "Assets/Resources/Item/Data";
        if (!Directory.Exists(targetFolder)) {
            Directory.CreateDirectory(targetFolder);
        }

        for (int i = 1; i < lines.Length; i++) {
            string[] cols = lines[i].Split(',');
            if (cols.Length < 3) continue;

            string itemCode = cols[0];
            string itemName = cols[1];
            string description = cols[2];
            ItemType itemType = (ItemType)System.Enum.Parse(typeof(ItemType),cols[3]);
            Sprite icon = IconParse(cols[4]);
            string prefabName = cols[5];
            GameObject prefab = PrefabParse(prefabName);
            bool isStackable = bool.Parse(cols[6]);
            int maxStack = int.Parse(cols[7]);
            string consumableType = cols[8];
            string consumableValue = cols[9];
            ItemDataConsumable[] consumables = ConsumableParse(consumableType, consumableValue);
            
            string assetPath = $"{targetFolder}/{prefabName}.asset";

            // 이미 존재하면 스킵 또는 덮어쓰기
            var existing = AssetDatabase.LoadAssetAtPath<ItemData>(assetPath);
            if (existing != null) {
                existing.itemCode =  itemCode;
                existing.displayName = itemName;
                existing.description = description;
                existing.type = itemType;
                existing.icon = icon;
                existing.prefab = prefab;
                existing.isStackable = isStackable; 
                existing.maxStack = maxStack;
                existing.consumables = consumables;
                
                EditorUtility.SetDirty(existing);
                continue;
            }
            var item = ScriptableObject.CreateInstance<ItemData>();
            item.itemCode =  itemCode;
            item.displayName = itemName;
            item.description = description;
            item.type = itemType;
            item.icon = icon;
            item.prefab = prefab;
            item.isStackable = isStackable; 
            item.maxStack = maxStack;
            item.consumables = consumables;

            AssetDatabase.CreateAsset(item, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("CSV import completed.");
    }

    private static Sprite IconParse(string iconName)
    {
        Sprite icon = Resources.Load<Sprite>( $"Item/Icons/{iconName}");
        return icon;
    }
    
    private static GameObject PrefabParse(string prefabName)
    {
        GameObject prefab = Resources.Load<GameObject>($"Item/Prefabs/{prefabName}");
        return prefab;
    }

    private static ItemDataConsumable[] ConsumableParse(string consumableType, string consumableValue)
    {
        ItemDataConsumable[] consumables = null;
        if (consumableType.Contains("|"))
        {
            string[] consumableTypes = consumableType.Split("|");
            string[] consumableValues = consumableValue.Split("|");

            int typeCount = consumableTypes.Length;
            consumables = new ItemDataConsumable[typeCount];
        
            if (typeCount == consumableValues.Length)
            {
                for (int i = 0; i < typeCount; i++)
                {
                    ItemDataConsumable consumable = new ItemDataConsumable();
                    consumable.type = (ConsumableType) System.Enum.Parse(typeof(ConsumableType), consumableTypes[i]);
                    consumable.value = int.Parse(consumableValues[i]);
                    Debug.Log(consumableTypes[i] + ">"+(ConsumableType) System.Enum.Parse(typeof(ConsumableType), consumableTypes[i]));
                    consumables[i] = consumable;
                }
                return consumables;
            }
        }
        else if(consumableType != "" && !consumableType.Contains("|") && consumableType != null)
        {
            consumables = new ItemDataConsumable[1];
            
            ItemDataConsumable consumable = new ItemDataConsumable();
            consumable.type = (ConsumableType) System.Enum.Parse(typeof(ConsumableType), consumableType);
            consumable.value = int.Parse(consumableValue);
            
            consumables[0] = consumable;
        }
        
        return consumables;
    }
    #endregion


    #region 기타 object 생성
    [MenuItem("Tools/CSV Import/Import GameObejctData")]
    public static void ImportFromGameObjectDataCSV()
    {
        string path = EditorUtility.OpenFilePanel("Select CSV", "", "csv");
        if (string.IsNullOrEmpty(path)) return;

        string[] lines = File.ReadAllLines(path);
        if (lines.Length <= 1) {
            Debug.LogWarning("CSV has no data.");
            return;
        }

        string targetFolder = "Assets/Resources/GameObject/Data";
        if (!Directory.Exists(targetFolder)) {
            Directory.CreateDirectory(targetFolder);
        }

        for (int i = 1; i < lines.Length; i++) {
            string[] cols = lines[i].Split(',');
            if (cols.Length < 3) continue;

            string itemCode = cols[0];
            string itemName = cols[1];
            string displayName = cols[2];
            string description = cols[3];
            
            string assetPath = $"{targetFolder}/{itemName}.asset";

            // 이미 존재하면 스킵 또는 덮어쓰기
            var existing = AssetDatabase.LoadAssetAtPath<InspectableData>(assetPath);
            if (existing != null) {
                existing.code =  itemCode;
                existing.displayName = displayName;
                existing.description = description;
                
                EditorUtility.SetDirty(existing);
                continue;
            }
            var item = ScriptableObject.CreateInstance<InspectableData>();
            item.code =  itemCode;
            item.displayName = displayName;
            item.description = description;

            AssetDatabase.CreateAsset(item, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("CSV import completed.");
    }

    #endregion
    
}
#endif