namespace Project.Scripts.BehaviorTree
{
    public class StalkNode : BTNode
    {
        public override bool Execute(AIController ai)
        {
            if (ai.CanDetectPlayer())
            {
                ai.Stalk();
                return true;
            }
            return false;
        }
    }

}