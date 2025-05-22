using System;
using UnityEngine;

public interface IJumpable
{
    void Launch(float force);
}
public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    
    private void Reset()
    {
        jumpForce = 100f;
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
}
