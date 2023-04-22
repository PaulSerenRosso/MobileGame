using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/InstantiateFXNodeDataSO",
        fileName = "new Tree_T_InstantiateFX_Spe_Data")]
    public class TaskInstantiateFXNodeDataSO : ActionNodeDataSO
    {
        public GameObject ParticleGO;
        public int Count;
        
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PoolService, BehaviorTreeEnums.TreeExternValues.GridManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskInstantiateFXNode);
        }
    }
}