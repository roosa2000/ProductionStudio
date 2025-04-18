using System.Collections.Generic;

namespace Project.Scripts.BehaviorTreeV2
{
    public class BTSequence : BTNode
    {
        private List<BTNode> _children;
        
        public BTSequence(List<BTNode> children)
        {
            _children = children;
        }

        public override BTStatus Evaluate()
        {
            foreach (var child in _children)
            {
                var status = child.Evaluate();
                if (status == BTStatus.Failure)
                {
                    Status = BTStatus.Failure;
                    return Status;
                }
                if (status == BTStatus.Running)
                {
                    Status = BTStatus.Running;
                    return Status;
                }
            }
            Status = BTStatus.Success;
            return Status;
        }
    }
}