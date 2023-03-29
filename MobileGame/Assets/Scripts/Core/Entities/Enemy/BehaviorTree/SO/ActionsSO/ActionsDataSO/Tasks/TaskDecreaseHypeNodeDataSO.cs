using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.SO
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/DecreaseHypeNodeDataSO",
        fileName = "new T_DecreaseHypeNode_Spe_Data")]
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
            return typeof(TaskDecreaseHypeNodeSO);
        }
    }
}
