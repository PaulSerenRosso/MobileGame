using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/GetMovePointOfLineWithCircleNodeDataSO",
        fileName = "new GetMovePointOfLineWithCircleNodeDataSO")]
    public class GetMovePointOfLineWithCircleNodeDataSO : ActionNodeDataSO
    {
        public int CircleIndex;

        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(GetMovePointOfLineWithCircleNode);
        }
    }
}