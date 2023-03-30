using System;
using BehaviorTree.Nodes.Actions;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.SO
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/DecreaseHypeNodeDataSO",
        fileName = "new T_DecreaseHype_Spe_Data")]
    public class TaskDecreaseHypeNodeDataSO : ActionNodeDataSO
    {
        public float HypeAmount;
        public bool IsUpdated;

        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.HypeService };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskDecreaseHypeNode);
        }
    }
}