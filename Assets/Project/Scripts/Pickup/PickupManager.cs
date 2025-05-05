using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public static PickupManager Instance;

    private int counter = 0;
    [SerializeField] private int maxPickups = 3;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterPickup()
    {
        counter++;
        Debug.Log("Pickups collected: " + counter);
    }

    public bool HasCollectedAll()
    {
        return counter >= maxPickups;
    }

    //public int GetPickupCount() => counter;
}
