using System;
using UnityEngine;

namespace BehaviorTree.Struct
{
    public abstract class StructNodeSO : ScriptableObject
    {
        public abstract Type GetTypeNode();
    }
}