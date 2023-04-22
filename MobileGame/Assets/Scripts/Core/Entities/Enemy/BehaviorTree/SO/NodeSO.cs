using System;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class NodeSO : ScriptableObject
    {
        [TextArea] public string Comment;
       public bool isNeededToDuplicate = true;
        public abstract Type GetTypeNode();

        protected virtual void OnValidate()
        {
            UpdateComment();
        }

        public abstract void UpdateComment();
    }
}