using System;
using BehaviorTree.Nodes.Actions;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.SO
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/MoveHypeNodeDataSO",
        fileName = "new T_DecreasePlayerHype_Spe_Data")]
    public class TaskMoveHypeNodeDataSO : ActionNodeDataSO
    {
        public float HypeAmount;
        public BehaviorTreeEnums.HypeFunctionMode HypeFunctionMode;
        public bool IsUpdated;

        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.HypeService };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskMoveHypeNode);
        }
    }
}