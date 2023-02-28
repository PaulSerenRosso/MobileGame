using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/GetMovePointOfLineDataSO",
        fileName = "new GetMovePointOfLineDataSO")]
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