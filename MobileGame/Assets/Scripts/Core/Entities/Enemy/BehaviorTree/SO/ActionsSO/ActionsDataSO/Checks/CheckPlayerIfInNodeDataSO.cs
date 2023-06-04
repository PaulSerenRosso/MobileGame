using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/PlayerIfInDataSO",
        fileName = "new Tree_CH_PlayerIfIn_Spe_Data")]
    public class CheckPlayerIfInNodeDataSO : ActionNodeDataSO
    {
        public bool isCurrentOrLastMovePoint;

        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PlayerMovementHandler };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckPlayerIfInNode);
        }
    }
}