using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;
using UnityEngine.Serialization;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SetStateDataSO", fileName = "new T_SetState_Spe_Data")]
    public class TaskSetStateNodeDataSO : ActionNodeDataSO
    {
        [FormerlySerializedAs("enemyMobilityState")] [FormerlySerializedAs("EnemyState")]
        public EnemyEnums.EnemyMobilityState EnemyMobilityState;

        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.EnemyManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskSetStateNode);
        }
    }
}