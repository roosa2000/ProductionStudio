using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        if (HealthSystem.Instance == null)
        {
            new HealthSystem(100);
        }
        
        slider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        slider.value = HealthSystem.Instance.GetHealthPercentage();
    }
}
