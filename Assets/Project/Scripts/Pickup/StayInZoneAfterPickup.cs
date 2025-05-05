using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInZoneAfterPickup : MonoBehaviour
{
    private bool playerInside = false;
    private float stayTimer = 0f;
    private bool canStartTimer = false;

    [SerializeField] private float requiredStayTime = 5f;

    private void Update()
    {
        if (!canStartTimer) return;

        if (playerInside)
        {
            stayTimer += Time.deltaTime;
            if (stayTimer >= requiredStayTime)
            {
                Debug.Log("Player stayed for 5 seconds! Victory or next event!");
                canStartTimer = false; // Prevent repeat
            }
        }
        else
        {
            stayTimer = 0f; // Reset if they leave
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PickupManager.Instance.HasCollectedAll())
        {
            playerInside = true;
            canStartTimer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

}
