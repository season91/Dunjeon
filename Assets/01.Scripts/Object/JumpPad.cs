using System;
using UnityEngine;
/// <summary>
/// [컴포넌트]
/// </summary>

public class JumpPad : MonoBehaviour, IInspectable
{
    [SerializeField] private float jumpForce;
    public InspectableData data;
    
    private void Reset()
    {
        jumpForce = 100f;
        // csv import 선행 작업 필요
        data = Resources.Load<InspectableData>($"GameObject/Data/{name}");
    }

    private void OnCollisionEnter(Collision other)
    {
        // 위에서 밟는 경우
        foreach (ContactPoint contact in other.contacts)
        {
            Vector3 normal = contact.normal;

            if (Vector3.Dot(-normal, Vector3.up) > 0.1f) 
            {
                if (other.collider.GetComponentInChildren<IJumpable>() is IJumpable jumpable)
                {
                    jumpable.Launch(jumpForce);
                }
                break;
            }
        }
    }

    public string GetPromptText()
    {
        string str = $"\n{data.displayName}\n{data.description}";
        return str;
    }
}
