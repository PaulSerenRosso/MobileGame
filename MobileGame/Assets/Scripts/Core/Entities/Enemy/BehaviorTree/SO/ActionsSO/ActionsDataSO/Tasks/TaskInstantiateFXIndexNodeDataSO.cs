using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/InstantiateFXIndexNodeDataSO",
        fileName = "new Tree_T_InstantiateFXIndex_Spe_Data")]
    public class TaskInstantiateFXIndexNodeDataSO : ActionNodeDataSO
    {
        public GameObject ParticleGO;
        public int Count;
        
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.GridManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskInstantiateFXIndexNode);
        }
    }
}