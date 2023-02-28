using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/CheckCirclesPlayerNodeDataSO",
        fileName = "new CheckCirclesPlayerNodeDataSO")]
    public class CheckCirclesPlayerNodeDataSO : ActionNodeDataSO
    {
        public int[] CirclesIndexes;

        protected override void SetDependencyValues()
        {
            ExternValues = new[]
            {
                BehaviourTreeEnums.TreeExternValues.PlayerHandlerMovement,
                BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager
            };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckCirclesPlayerNode);
        }
    }
}