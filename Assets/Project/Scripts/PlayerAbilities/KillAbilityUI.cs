using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class KillAbilityUI : MonoBehaviour
{
    [SerializeField] private Image iconBorder;
    [SerializeField] private Image executeIcon;
    [SerializeField] private TMP_Text executeText;

    public void ShowIcon()
    {
        // if (iconBorder != null && executeIcon != null && executeText != null)
        // {
        //     var iconBorderColor = iconBorder.color;
        //     iconBorderColor.a = 1f;
        //     iconBorder.color = iconBorderColor;
        //     
        //     var executeIconColor = executeIcon.color;
        //     executeIconColor.a = 1f;
        //     executeIcon.color = executeIconColor;
        //     
        //     var executeTextColor = executeText.color;
        //     executeTextColor.a = 1f;
        //     executeText.color = executeTextColor;
        // }
        iconBorder.gameObject.SetActive(true);
    }
    
    public void HideIcon()
    {
        // if (iconBorder != null && executeIcon != null && executeText != null)
        // {
        //     var iconBorderColor = iconBorder.color;
        //     iconBorderColor.a = 0f;
        //     iconBorder.color = iconBorderColor;
        //     
        //     var executeIconColor = executeIcon.color;
        //     executeIconColor.a = 0f;
        //     executeIcon.color = executeIconColor;
        //     
        //     var executeTextColor = executeText.color;
        //     executeTextColor.a = 0f;
        //     executeText.color = executeTextColor;
        // }
        
        iconBorder.gameObject.SetActive(false);
    }
}
