using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/SendPlayerMovePointIndexNodeDataSO",
        fileName = "new SendPlayerMovePointIndexNodeDataSO")]
    public class SendPlayerMovePointIndexNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviourTreeEnums.TreeExternValues.PlayerHandlerMovement };
        }

        public override Type GetTypeNode()
        {
            return typeof(SendPlayerMovePointIndexNode);
        }
    }
}