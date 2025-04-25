namespace Project.Scripts.BehaviorTree
{
    public class AvoidLightNode : BTNode
    {
        public override bool Execute(AIController ai)
        {
            if (ai.IsNearLight())
            {
                ai.Retreat();
                return true;
            }
            return false;
        }
    }
}