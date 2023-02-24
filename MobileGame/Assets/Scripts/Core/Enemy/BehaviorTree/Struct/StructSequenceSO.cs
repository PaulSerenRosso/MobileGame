using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using BehaviorTree.Nodes;
using UnityEngine;

namespace BehaviorTree.Struct
{
    [CreateAssetMenu(menuName = "BehaviorTree/StructSequenceSO", fileName = "new StructSequenceSO")]
    public class StructSequenceSO : InnerNodeStructSO
    {
        public override Type GetTypeNode()
        {
            return typeof(Sequence);
        }
    }
}
