using System.Collections.Generic;
using Project.Scripts.BehaviorTreeV2.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.BehaviorTreeV2
{
    public class AIAgent : MonoBehaviour
    {
        [SerializeField] private AIConfig config;
        private IAgentMemory memory;
        private BTNode root;

        [SerializeField] private Transform player;

        private BTNode currentRunningNode;
        
        [SerializeField] private float roamRadius = 10f;
        [SerializeField] private float roamCooldown = 2f; // Time to wait before choosing next point

        private float roamTimer;
        private bool isRoaming = false;
        private Vector3 roamTarget;
        private NavMeshAgent agent;
        
        float stuckTimer = 0f;
        float maxStuckTime = 5f;

        bool isPaused = false;
        float pauseTimer = 0f;
        float pauseDuration = 2f; // Pause for 2 seconds between destinations
        
        private Animator animator;

        private void Start()
        {
            memory = new AgentMemory();
            memory.SetTransform("Player", player);
            memory.SetFloat("Health", 100f);
            memory.SetBool("IsPlayerDetected", false);
            agent = GetComponent<NavMeshAgent>();
            root = BuildBehaviorTree();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
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

                Transform playerTransform = memory.GetTransform("Player");

                float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

                if (distanceToPlayer <= config.AttackRange)
                    return BTStatus.Success;

                if (agent != null)
                    agent.SetDestination(playerTransform.position);

                Debug.DrawLine(transform.position, playerTransform.position, Color.red); // Visual line during chase

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
                roamTimer += Time.deltaTime;

                // Handle pause between destinations
                if (isPaused)
                {
                    pauseTimer += Time.deltaTime;
                    if (pauseTimer >= pauseDuration)
                    {
                        isPaused = false;
                        pauseTimer = 0f;
                        roamTimer = roamCooldown; // Trigger new roam point after pause
                    }

                    return BTStatus.Running;
                }

                if (!agent.pathPending &&
                    (agent.remainingDistance <= agent.stoppingDistance) &&
                    (!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete))
                {
                    stuckTimer = 0f;

                    if (!isRoaming && roamTimer >= roamCooldown)
                    {
                        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
                        randomDirection += transform.position;

                        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, roamRadius, NavMesh.AllAreas))
                        {
                            roamTarget = hit.position;
                            agent.SetDestination(roamTarget);
                            isRoaming = true;
                            roamTimer = 0f;
                            Debug.DrawLine(transform.position, roamTarget, Color.green, 2f);
                        }
                    }
                    else if (isRoaming)
                    {
                        // Reached target
                        isRoaming = false;
                        isPaused = true; // Trigger idle pause before next destination
                    }
                }
                else
                {
                    stuckTimer += Time.deltaTime;

                    if (stuckTimer >= maxStuckTime)
                    {
                        Debug.LogWarning("Agent might be stuck. Picking a new roam target.");
                        isRoaming = false;
                        isPaused = false;
                        roamTimer = roamCooldown;
                        stuckTimer = 0f;
                    }
                }

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
        
        private void OnDrawGizmosSelected()
        {
            if (config == null) return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, config.DetectionRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, config.AttackRange);
        }
    }
    
    
}