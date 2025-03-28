namespace Project.Scripts.BehaviorTree
{
    public class IdleNode : BTNode
    {
        public override bool Execute(AIController ai)
        {
            ai.Idle();
            return true;
        }
    }
}