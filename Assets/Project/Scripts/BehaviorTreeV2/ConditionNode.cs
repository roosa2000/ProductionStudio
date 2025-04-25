using System;

namespace Project.Scripts.BehaviorTreeV2
{
    public class ConditionNode : BTNode
    {
        private Func<bool> _condition;
        public ConditionNode(Func<bool> condition)
        {
            _condition = condition;
        }

        public override BTStatus Evaluate()
        {
            return _condition.Invoke() ? BTStatus.Success : BTStatus.Failure;
        }
    }
}