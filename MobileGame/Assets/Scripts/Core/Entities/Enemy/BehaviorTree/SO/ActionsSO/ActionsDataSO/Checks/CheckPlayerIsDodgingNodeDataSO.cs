using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/PlayerIsDodgingDataSO", fileName = "new Tree_CH_PlayerIsDodging_Spe_Data")]
    public class CheckPlayerIsDodgingNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PlayerHandlerMovement };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckPlayerIsDodgingNode);
        }
    }
}