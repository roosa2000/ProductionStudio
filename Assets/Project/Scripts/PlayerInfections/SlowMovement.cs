using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class SlowMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private float minSpeedMultiplier = 0.2f;
    private FirstPersonController firstPersonController;

    private void Awake()
    {
        firstPersonController = playerTransform.GetComponent<FirstPersonController>();
    }
    void Update()
    {
        float healthPercent = HealthSystem.Instance.GetHealthPercentage();
        float speedMultiplier = Mathf.Lerp(minSpeedMultiplier, 1f, healthPercent);
        firstPersonController.SetSpeedMultiplier(speedMultiplier);
    }
}
