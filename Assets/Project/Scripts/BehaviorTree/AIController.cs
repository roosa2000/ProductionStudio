using UnityEngine;
using System.Collections.Generic;
using Project.Scripts.BehaviorTree;

public class AIController : MonoBehaviour
{
    public AIBehaviorConfig config; // Assign different configs for Vampires & Werewolves

    private List<BTNode> behaviorTree;

    void Start()
    {
        behaviorTree = new List<BTNode>
        {
            new AvoidLightNode(),
            new AmbushNode(),
            new StalkNode(),
            new IdleNode()
        };

        if (IsWerewolf()) 
        {
            behaviorTree.Add(new HowlNode());
            behaviorTree.Add(new EnrageNode());
        }
    }

    void Update()
    {
        foreach (var node in behaviorTree)
        {
            if (node.Execute(this))
                break;  // Stop after executing the highest priority action
        }
    }

    public bool IsWerewolf() => config.howlRadius > 0; // Simple check to differentiate

    // Detection
    public bool IsNearLight() => config.avoidsLight  /* Logic to detect light */;
    public bool CanAmbush() => config.ambushDamage /* Logic to check if player is unaware */;
    public bool CanDetectPlayer() => config.detectionRange/* Logic to check if player is in range */;
    public bool IsLowHealth() => config.enragesAtLowHealth/* Logic to check if health is low */;

    // Actions
    public void Retreat() => Debug.Log("Retreating from light!");
    public void Ambush() => Debug.Log("Ambushing the player!");
    public void Stalk() => Debug.Log("Stalking the player...");
    public void Idle() => Debug.Log("Hiding in the dark.");
    public void Howl() => Debug.Log("Werewolf howling to summon reinforcements!");
    public void Enrage() => Debug.Log("Werewolf enrages, increasing attack speed!");
}