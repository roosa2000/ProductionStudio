namespace Project.Scripts.BehaviorTree
{
    public class EnrageNode : BTNode
    {
        public override bool Execute(AIController ai)
        {
            if (ai.IsWerewolf() && ai.IsLowHealth())
            {
                ai.Enrage();
                return true;
            }
            return false;
        }
    }
}