using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private int healAmount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem.Instance.Heal(healAmount);
            Destroy(other.gameObject);
        }
    }
}
