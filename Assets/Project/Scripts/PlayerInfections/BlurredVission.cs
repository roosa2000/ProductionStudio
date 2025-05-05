using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlurredVission : MonoBehaviour
{
    [SerializeField] private Image overlayImage; // Assign the UI Image in Inspector
    private float maxAlpha = 0.9f; // Max opacity when health is 0
    void Update()
    {
        float healthPercent = HealthSystem.Instance.GetHealthPercentage();
        float currentAlpha = overlayImage.color.a;
        float targetAlpha = (1f - healthPercent) * maxAlpha;
        float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * 5f); // 5 is the speed

        Color currentColor = overlayImage.color;
        currentColor.a = newAlpha;
        overlayImage.color = currentColor;
    }
}
