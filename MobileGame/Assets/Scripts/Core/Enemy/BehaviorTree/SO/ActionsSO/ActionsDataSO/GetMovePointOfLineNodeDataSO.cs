using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/GetMovePointOfLineNodeDataSO",
        fileName = "new GetMovePointOfLineNodeDataSO")]
    public class GetMovePointOfLineNodeDataSO : ActionNodeDataSO
    {
        public int indexMovedAmount;
        
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(GetMovePointOfLineNode);
        }
    }
}