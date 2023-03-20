using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    public class TaskInstantiateFXNodeDataSO : ActionNodeDataSO
    {
        public GameObject ParticleGO;
        public int Count;
        
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviourTreeEnums.TreeExternValues.PoolService };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskInstantiateFXNode);
        }
    }
}