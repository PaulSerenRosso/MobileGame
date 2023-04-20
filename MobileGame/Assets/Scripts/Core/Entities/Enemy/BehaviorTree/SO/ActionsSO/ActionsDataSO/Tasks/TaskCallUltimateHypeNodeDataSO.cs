using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/CallUltimateHypeNodeDataSO",
        fileName = "new T_CallUltimateHype_Spe_Data")]
    public class TaskCallUltimateHypeNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            EnemyValues = new[]
            {
                BehaviorTreeEnums.TreeEnemyValues.EnemyManager
            };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskCallUltimateHypeNode);
        }
    }
}