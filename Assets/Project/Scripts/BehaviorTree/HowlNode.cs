namespace Project.Scripts.BehaviorTree
{
    public class HowlNode : BTNode
    {
        public override bool Execute(AIController ai)
        {
            if (ai.IsWerewolf() && ai.CanDetectPlayer())
            {
                ai.Howl();
                return true;
            }
            return false;
        }
    }
}