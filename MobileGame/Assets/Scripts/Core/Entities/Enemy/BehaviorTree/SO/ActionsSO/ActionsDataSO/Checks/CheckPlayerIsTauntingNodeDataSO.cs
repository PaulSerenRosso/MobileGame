using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/PlayerIsTauntingDataSO",
        fileName = "new Tree_CH_PlayerIsTaunting_Spe_Data")]
    public class CheckPlayerIsTauntingNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PlayerTauntHandler };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckPlayerIsTauntingNode);
        }
    }
}