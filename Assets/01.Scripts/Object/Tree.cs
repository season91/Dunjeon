using UnityEngine;

public class Tree : MonoBehaviour, IInspectable
{
    public InspectableData data;
    private void Reset()
    {
        data = Resources.Load<InspectableData>($"GameObject/Data/{name}");
    }
    
    public string GetPromptText()
    {
        string str = $"\n{data.displayName}\n{data.description}";
        return str;
    }
}
