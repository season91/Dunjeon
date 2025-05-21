using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;

    private void Reset()
    {
        // csv import 선행 작업 필요
        data = Resources.Load<ItemData>($"Item/Data/{this.name}");
    }
}
