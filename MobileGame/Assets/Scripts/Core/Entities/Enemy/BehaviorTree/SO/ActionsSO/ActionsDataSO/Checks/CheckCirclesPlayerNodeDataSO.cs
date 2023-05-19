using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/CirclesPlayerNodeDataSO",
        fileName = "new Tree_CH_CirclesPlayer_Spe_Data")]
    public class CheckCirclesPlayerNodeDataSO : ActionNodeDataSO
    {
        public int[] CirclesIndexes;

        protected override void SetDependencyValues()
        {
            ExternValues = new[]
            {
                BehaviorTreeEnums.TreeExternValues.PlayerMovementHandler,
                BehaviorTreeEnums.TreeExternValues.GridManager
            };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckCirclesPlayerNode);
        }
    }
}