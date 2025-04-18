using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.BehaviorTreeV2
{
    public class AgentMemory : IAgentMemory
    {
        private Dictionary<string, object> _data = new();
        public bool GetBool(string key) => _data.ContainsKey(key) && (bool)_data[key];
        public float GetFloat(string key) => _data.ContainsKey(key) ? (float)_data[key] : 0f;
        public Transform GetTransform(string key) => _data.ContainsKey(key) ? _data[key] as Transform : null;

        public void SetBool(string key, bool value) => _data[key] = value;
        public void SetFloat(string key, float value) => _data[key] = value;
        public void SetTransform(string key, Transform value) => _data[key] = value;
    }
}