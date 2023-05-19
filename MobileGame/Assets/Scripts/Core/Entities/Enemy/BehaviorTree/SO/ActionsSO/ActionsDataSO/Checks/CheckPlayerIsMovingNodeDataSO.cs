using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/PlayerIsMovingDataSO",
        fileName = "new Tree_CH_PlayerIsMoving_Spe_Data")]
    public class CheckPlayerIsMovingNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PlayerMovementHandler };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckPlayerIsMovingNode);
        }
    }
}