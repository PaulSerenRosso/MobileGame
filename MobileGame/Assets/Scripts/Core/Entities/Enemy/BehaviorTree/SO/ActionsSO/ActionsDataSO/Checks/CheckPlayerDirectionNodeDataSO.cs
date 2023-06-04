using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/PlayerDirectionDataSO",
        fileName = "new Tree_CH_PlayerDirection_Spe_Data")]
    public class CheckPlayerDirectionNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.Transform };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckPlayerDirectionNode);
        }
    }
}