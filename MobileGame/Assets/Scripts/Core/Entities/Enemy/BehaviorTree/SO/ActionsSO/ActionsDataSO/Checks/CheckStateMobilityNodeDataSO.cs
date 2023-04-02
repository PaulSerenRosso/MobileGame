using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/StateMobilityDataSO", fileName = "new CH_StateMobility_Spe_Data")]
    public class CheckStateMobilityNodeDataSO : ActionNodeDataSO
    {
        public EnemyEnums.EnemyMobilityState enemyMobilityState;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.EnemyManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckStateMobilityNode);
        }
    }
}