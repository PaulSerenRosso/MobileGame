using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SetStateMobilityDataSO",
        fileName = "new T_SetStateMobility_Spe_Data")]
    public class TaskSetStateMobilityNodeDataSO : ActionNodeDataSO
    {
        public EnemyEnums.EnemyMobilityState EnemyMobilityState;

        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.EnemyManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskSetStateMobilityNode);
        }
    }
}