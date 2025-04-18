using UnityEngine;

namespace Project.Scripts.BehaviorTreeV2.ScriptableObjects
{
    [CreateAssetMenu(menuName = "AI/AIConfig")]
    public class AIConfig: ScriptableObject
    {
        public float DetectionRange;
        public float AttackRange;
        public float Damage;
        public bool CanRoam; 
        public float FleeDistance { get; set; }
    }
}