using System.Reflection;
using UnityEngine;

namespace Project.Scripts.BehaviorTreeV2
{
    public class ActionNodeWrapper : BTNode
    {
        private MonoBehaviour _component;
        private string _methodName;
        public ActionNodeWrapper(MonoBehaviour component, string methodName)
        {
            _component = component;
            _methodName = methodName;
        }

        public override BTStatus Evaluate()
        {
            var method = _component.GetType().GetMethod(_methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (method == null)
            {
                Debug.LogError($"Method '{_methodName}' not found on {_component.GetType().Name}");
                return BTStatus.Failure;
            }

            var result = method.Invoke(_component, null);

            if (result is BTStatus status)
                return status;

            return BTStatus.Success;
        }
    }
}