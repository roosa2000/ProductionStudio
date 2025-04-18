using System.Collections.Generic;
using Project.Scripts.BehaviorTreeV2.ScriptableObjects;
using UnityEngine;

namespace Project.Scripts.BehaviorTreeV2
{
    public class AIAgent : MonoBehaviour
    {
        [SerializeField] private AIConfig config;
        private IAgentMemory memory;
        private BTNode root;

        [SerializeField] private Transform player;

        private BTNode currentRunningNode;

        private void Start()
        {
            memory = new AgentMemory();
            memory.SetTransform("Player", player);
            memory.SetFloat("Health", 100f);
            memory.SetBool("IsPlayerDetected", false);

            root = BuildBehaviorTree();
        }

        private void Update()
        {
            if (currentRunningNode != null)
            {
                var status = currentRunningNode.Evaluate();

                if (status != BTStatus.Running)
                {
                    currentRunningNode = null;
                }

                return; // Still busy doing something, don't start new actions.
            }

            var statusFromRoot = root.Evaluate();

            // If something new started running, set it as current
            if (statusFromRoot == BTStatus.Running)
            {
                currentRunningNode = root;
            }
        }

        private BTNode BuildBehaviorTree()
        {
            // ===== FLEE SEQUENCE =====
            var lowHealthCondition = new ConditionNode(() => memory.GetFloat("Health") < 20);

            var fleeAction = new ActionNode(() => {
                Debug.Log("Fleeing from player");
                // Your flee logic here...
                if (Vector3.Distance(transform.position, memory.GetTransform("Player").position) > config.FleeDistance)
                    return BTStatus.Success;

                // Move away logic...
                return BTStatus.Running;
            });

            var fleeSequence = new BTSequence(new List<BTNode> {
                lowHealthCondition, fleeAction
            });

            // ===== COMBAT SEQUENCE =====
            var attackCondition = new ConditionNode(() =>
                Vector3.Distance(transform.position, memory.GetTransform("Player").position) <= config.AttackRange);

            var attackAction = new ActionNode(() => {
                Debug.Log("Attacking player");

                if (Vector3.Distance(transform.position, memory.GetTransform("Player").position) > config.AttackRange)
                    return BTStatus.Success;

                // Play attack animation, damage, etc...
                return BTStatus.Running;
            });

            var combatSequence = new BTSequence(new List<BTNode> {
                attackCondition, attackAction
            });

            // ===== CHASE SEQUENCE =====
            var detectCondition = new ConditionNode(() => {
                var distance = Vector3.Distance(transform.position, memory.GetTransform("Player").position);
                bool detected = distance <= config.DetectionRange;

                if (detected)
                    memory.SetBool("IsPlayerDetected", true);

                return memory.GetBool("IsPlayerDetected");
            });

            var chaseAction = new ActionNode(() => {
                Debug.Log("Chasing player");

                if (Vector3.Distance(transform.position, memory.GetTransform("Player").position) <= config.AttackRange)
                    return BTStatus.Success;

                // NavMeshAgent.SetDestination(player.position) or equivalent...
                return BTStatus.Running;
            });

            var chaseSequence = new BTSequence(new List<BTNode> {
                detectCondition, chaseAction
            });

            // ===== ROAMING SELECTOR =====
            var idleCondition = new ConditionNode(() => !config.CanRoam);
            var idleAction = new ActionNode(() => {
                Debug.Log("Idling");
                return BTStatus.Running;
            });

            var patrolCondition = new ConditionNode(() => config.CanRoam);
            var patrolAction = new ActionNode(() => {
                Debug.Log("Patrolling");
                return BTStatus.Running;
            });

            var roamingSelector = new BTSelector(new List<BTNode> {
                new BTSequence(new List<BTNode>{ idleCondition, idleAction }),
                new BTSequence(new List<BTNode>{ patrolCondition, patrolAction })
            });

            // ===== ROOT SELECTOR =====
            return new BTSelector(new List<BTNode> {
                fleeSequence,
                combatSequence,
                chaseSequence,
                roamingSelector
            });
        }
    }
}