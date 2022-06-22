using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpTextBehavior : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUIComponent;

    private void Awake()
    {
        textMeshProUGUIComponent = GetComponent<TextMeshProUGUI>();
    }

    public void resetAlpha() 
    {
        textMeshProUGUIComponent.color = new Color(textMeshProUGUIComponent.color.r, textMeshProUGUIComponent.color.g, textMeshProUGUIComponent.color.b, 0.99f);
    }

    // Update is called once per frame
    void Update()
    {
        if (textMeshProUGUIComponent.color.a <= 0) { this.gameObject.SetActive(false); }
        textMeshProUGUIComponent.color = new Color(textMeshProUGUIComponent.color.r, textMeshProUGUIComponent.color.g, textMeshProUGUIComponent.color.b, textMeshProUGUIComponent.color.a - (1- textMeshProUGUIComponent.color.a)*1.5f*Time.deltaTime);
    }
}
