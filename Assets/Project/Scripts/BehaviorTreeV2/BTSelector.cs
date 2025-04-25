using System.Collections.Generic;

namespace Project.Scripts.BehaviorTreeV2
{
    public class BTSelector : BTNode
    {
        private List<BTNode> _children;
        
        public BTSelector(List<BTNode> children)
        {
            _children = children;
        }

        public override BTStatus Evaluate()
        {
            foreach (var child in _children)
            {
                var status = child.Evaluate();
                if (status == BTStatus.Success || status == BTStatus.Running)
                {
                    Status = status;
                    return Status;
                }
            }
            Status = BTStatus.Failure;
            return Status;
        }
    }
}