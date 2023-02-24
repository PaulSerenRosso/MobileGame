using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Struct
{
    public abstract class StructNodeSO : ScriptableObject
    {
        public abstract Type GetTypeNode();
    }
}