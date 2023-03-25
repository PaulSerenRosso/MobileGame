using System;
using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class ActionNodeSO : NodeSO
    {
        [SerializeField] protected byte _internValuesCount;
        public ActionNodeDataSO Data;
        public List<InternValue> InternValues = new();

        public override Type GetTypeNode()
        {
            return Data.GetTypeNode();
        }

        public virtual void UpdateInterValues()
        {
            foreach (var internValue in InternValues)
            {
                internValue.UpdateKeyHashCode();
            }
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            UpdateInterValues();
        }
    }
}