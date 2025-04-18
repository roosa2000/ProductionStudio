using System;

namespace Project.Scripts.BehaviorTreeV2
{
    public class ActionNode : BTNode
    {
        private Func<BTStatus> _action;
        public ActionNode(Func<BTStatus> action)
        {
            _action = action;
        }

        public override BTStatus Evaluate()
        {
            return _action.Invoke();
        }
    }
}