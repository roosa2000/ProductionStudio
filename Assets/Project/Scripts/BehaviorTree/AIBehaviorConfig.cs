using UnityEngine;

[CreateAssetMenu(fileName = "NewAIConfig", menuName = "AI/BehaviorConfig")]
public class AIBehaviorConfig : ScriptableObject
{
    [Header("General Settings")]
    public bool detectionRange;
    public float movementSpeed;
    public float attackCooldown;

    [Header("Vampire-Specific")]
    public bool avoidsLight;
    public bool ambushDamage;

    [Header("Werewolf-Specific")]
    public float howlRadius;
    public bool enragesAtLowHealth;
}