using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int counter = 0; // Tracks the number of pickups collected
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            counter++;
            UpdateCounterUI();
            Destroy(other.gameObject); // Destroy the pickup item
        }
    }

    private void UpdateCounterUI()
    {
        Debug.Log("Pickups: " + counter);
    }
}
