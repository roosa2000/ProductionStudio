namespace Project.Scripts.BehaviorTree
{
    public class AmbushNode : BTNode
    {
        public override bool Execute(AIController ai)
        {
            if (ai.CanAmbush())
            {
                ai.Ambush();
                return true;
            }
            return false;
        }
    }
}