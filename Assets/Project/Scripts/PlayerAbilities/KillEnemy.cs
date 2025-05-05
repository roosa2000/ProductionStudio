using System.Collections;
using System.Collections.Generic;
using Project.Scripts.BehaviorTreeV2;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    [SerializeField] private float executeRange = 5f;
    [SerializeField] private LayerMask enemyLayer;

    private bool hasAbility = false;
    private KeyCode executeKey = KeyCode.E;
    [SerializeField] private KillAbilityUI abilityUI;
    
    private void Update()
    {
        if (hasAbility && Input.GetKeyDown(executeKey))
        {
            ExecuteEnemiesInRange();
            hasAbility = false; // Burn it after use
            abilityUI.HideIcon();
        }
    }
    
    private void ExecuteEnemiesInRange()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, executeRange, enemyLayer);

        foreach (Collider enemyCol in enemies)
        {
            AIAgent enemy = enemyCol.GetComponent<AIAgent>();
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
                Debug.Log($"Executed: {enemy.gameObject.name}");
            }
        }
    }
    
    public void GrantAbility()
    {
        hasAbility = true;
        abilityUI.ShowIcon();
        Debug.Log("Execute ability granted!");
    }



}
