using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/CirclesPlayerNodeDataSO",
        fileName = "new CH_CirclesPlayer_Spe_Data")]
    public class CheckCirclesPlayerNodeDataSO : ActionNodeDataSO
    {
        public int[] CirclesIndexes;

        protected override void SetDependencyValues()
        {
            ExternValues = new[]
            {
                BehaviorTreeEnums.TreeExternValues.PlayerHandlerMovement,
                BehaviorTreeEnums.TreeExternValues.EnvironmentGridManager
            };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckCirclesPlayerNode);
        }
    }
}