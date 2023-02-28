using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/GetMovePointOfCircleDataSO", fileName = "new GetMovePointOfCircleDataSO")]
    public class GetMovePointOfCircleNodeDataSO : ActionNodeDataSO
    {
        public int IndexMovedAmount;
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(GetMovePointOfCircleNode);
        }
    }
}