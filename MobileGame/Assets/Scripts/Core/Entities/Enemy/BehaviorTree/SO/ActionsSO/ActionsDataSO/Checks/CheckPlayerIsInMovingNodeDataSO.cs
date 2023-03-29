using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/PlayerIsInMovingDataSO",
        fileName = "new CH_PlayerIsInMoving_Spe_Data")]
    public class CheckPlayerIsInMovingNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PlayerHandlerMovement };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckPlayerIsInMovingNode);
        }
    }
}