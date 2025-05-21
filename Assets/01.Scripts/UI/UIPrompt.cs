using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPrompt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;

    private void Reset()
    {
        promptText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowDescriptionPrompt(string text)
    {
        promptText.gameObject.SetActive(true);
        promptText.text = text;
    }
    
    public void HideDescriptionPrompt() => promptText.gameObject.SetActive(false);
}
