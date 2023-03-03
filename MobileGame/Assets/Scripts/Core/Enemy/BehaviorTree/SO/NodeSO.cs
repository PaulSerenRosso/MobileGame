using System;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class NodeSO : ScriptableObject
    {
        [TextArea] [SerializeField] private string _commentary;
        
        public abstract Type GetTypeNode();

        protected virtual void OnValidate()
        {
            UpdateCommentary();
        }

        public abstract void UpdateCommentary();
    }
}