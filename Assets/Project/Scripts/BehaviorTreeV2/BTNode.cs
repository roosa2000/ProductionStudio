namespace Project.Scripts.BehaviorTreeV2
{
    public abstract class BTNode
    {
        public BTStatus Status { get; protected set; } = BTStatus.Running;
        public abstract BTStatus Evaluate();
    }
}