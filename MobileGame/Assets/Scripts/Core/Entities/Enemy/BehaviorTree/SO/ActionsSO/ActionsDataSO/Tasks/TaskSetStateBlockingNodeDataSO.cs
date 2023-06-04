using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SetStateBlockingDataSO",
        fileName = "new Tree_T_SetStateBlocking_Spe_Data")]
    public class TaskSetStateBlockingNodeDataSO : ActionNodeDataSO
    {
        public EnemyEnums.EnemyBlockingState EnemyBlockingState;

        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.EnemyManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskSetStateBlockingNode);
        }
    }
}