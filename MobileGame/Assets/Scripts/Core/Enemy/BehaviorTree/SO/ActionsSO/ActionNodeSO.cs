using System;
using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class ActionNodeSO : NodeSO
    {
        public ActionNodeDataSO Data;

        public override Type GetTypeNode()
        {
            return Data.GetTypeNode();
        }

        public virtual void ConvertKeyOfInternValueToHashCode()
        {
            
        }

        private void OnValidate()
        {
            ConvertKeyOfInternValueToHashCode();
        }
    }
}