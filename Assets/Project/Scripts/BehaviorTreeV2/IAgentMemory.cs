using UnityEngine;

namespace Project.Scripts.BehaviorTreeV2
{
    public interface IAgentMemory
    {
        bool GetBool(string key);
        
        float GetFloat(string key);
        
        void SetBool(string key, bool value);
        
        void SetFloat(string key, float value);
        
        Transform GetTransform(string key);
        
        void SetTransform(string key, Transform value);
    }
}