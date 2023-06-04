using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SetBoostNodeDataSO",
        fileName = "new Tree_T_SetBoost_Spe_Data")]
    public class TaskSetBoostNodeDataSO : ActionNodeDataSO
    {
        public bool BooleanValue;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.EnemyManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskSetBoostNode);
        }
    }
}