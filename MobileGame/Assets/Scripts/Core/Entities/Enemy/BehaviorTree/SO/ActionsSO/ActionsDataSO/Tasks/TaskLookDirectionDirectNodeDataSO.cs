using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/LookDirectionDirectNodeDataSO",
        fileName = "new Tree_T_LookDirectionDirect_Spe_Data")]
    public class TaskLookDirectionDirectNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.Transform };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskLookDirectionDirectNode);
        }
    }
}