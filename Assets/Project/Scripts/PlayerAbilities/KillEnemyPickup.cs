using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KillEnemy ability = other.GetComponent<KillEnemy>();
            if (ability != null)
            {
                ability.GrantAbility();
                Destroy(gameObject);
            }
        }
    }
}
