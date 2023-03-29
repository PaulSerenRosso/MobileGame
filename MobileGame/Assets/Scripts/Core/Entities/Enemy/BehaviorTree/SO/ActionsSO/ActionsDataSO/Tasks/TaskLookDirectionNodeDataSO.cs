using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/LookDirectionNodeDataSO",
        fileName = "new T_LookDirection_Spe_Data")]
    public class TaskLookDirectionNodeDataSO : ActionNodeDataSO
    {
        public float TimeRotation;

        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.Transform };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskLookDirectionNode);
        }
    }
}