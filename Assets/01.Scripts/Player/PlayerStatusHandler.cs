using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// [요청/이벤트 처리] 상태 변경 요청 처리하는 로직
/// </summary>
public class PlayerStatusHandler : MonoBehaviour
{
    private Coroutine healthCoroutine;
    private Coroutine hungerCoroutine;
    
    private void Update()
    {
        // 테스트용 코드
        // UIManager.Instance.SubstractHealth(3* Time.deltaTime);
        UIManager.Instance.SubstractHunger(10* Time.deltaTime);
    }

    public void Heal(float value)
    {
        if (healthCoroutine != null)
        {
            StopCoroutine(healthCoroutine);
        }
        // 3초동안 회복
        healthCoroutine = StartCoroutine(GradualHealthRecovery(value, 3f));
    }

    private IEnumerator GradualHealthRecovery(float value, float duration)
    {
        float elapsed = 0f;
        float interval = 0.1f; // 0.1초 간격

        while (elapsed < duration)
        {
            UIManager.Instance.AddHealth(value);
            elapsed += interval;
            yield return new WaitForSeconds(interval);
        }

        healthCoroutine = null;
    }

    public void Hunger(float value)
    {
        if (hungerCoroutine != null)
        {
            StopCoroutine(hungerCoroutine);
        }
        // 3초동안 회복
        hungerCoroutine = StartCoroutine(GradualHungerRecovery(value, 3f)); // 3초간 회복
    }
    
    private IEnumerator GradualHungerRecovery(float value, float duration)
    {
        float elapsed = 0f;
        float interval = 0.1f; // 0.1초 간격

        while (elapsed < duration)
        {
            UIManager.Instance.AddHunger(value);
            elapsed += interval;
            yield return new WaitForSeconds(interval);
        }

        hungerCoroutine = null;
    }
        
    // 아이템 사용
    public void UseItem(ItemData item)
    {
        for (int i = 0; i < item.consumables.Length; i++)
        {
            switch (item.consumables[i].type)
            {
                case ConsumableType.Health:
                    Heal(item.consumables[i].value);
                    break;
                case ConsumableType.Hunger:
                    Hunger(item.consumables[i].value);
                    break;
            }
        }
    }
    
    // 아이템 장착

}
