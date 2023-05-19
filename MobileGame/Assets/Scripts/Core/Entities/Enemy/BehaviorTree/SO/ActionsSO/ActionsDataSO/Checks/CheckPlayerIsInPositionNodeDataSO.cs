using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/PlayerIsInPositionDataSO",
        fileName = "new Tree_CH_PlayerIsInPosition_Spe_Data")]
    public class CheckPlayerIsInPositionNodeDataSO : ActionNodeDataSO
    {
        public float Radius;

        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PlayerTransform };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckPlayerIsInPositionNode);
        }
    }
}