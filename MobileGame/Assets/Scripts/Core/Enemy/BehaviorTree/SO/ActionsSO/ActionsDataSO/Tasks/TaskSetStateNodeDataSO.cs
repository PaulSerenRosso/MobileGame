using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SetStateDataSO", fileName = "new T_SetState_Spe_Data")]
    public class TaskSetStateNodeDataSO : ActionNodeDataSO
    {
        public EnemyEnums.EnemyState EnemyState;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviourTreeEnums.TreeEnemyValues.EnemyManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskSetStateNode);
        }
    }
}