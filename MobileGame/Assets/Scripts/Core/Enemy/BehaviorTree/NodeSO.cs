using System;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class NodeSO : ScriptableObject
    {
        public abstract Type GetTypeNode();
    }
}