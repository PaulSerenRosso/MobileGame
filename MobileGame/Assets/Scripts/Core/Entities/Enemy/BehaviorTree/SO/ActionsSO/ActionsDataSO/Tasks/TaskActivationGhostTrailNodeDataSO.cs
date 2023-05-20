using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/ActivationGhostTrailNodeDataSO",
        fileName = "new Tree_T_ActivationGhostTrail_Spe_Data")]
    public class TaskActivationGhostTrailNodeDataSO : ActionNodeDataSO
    {
        public Vector2 Direction;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.EnemyManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskActivationGhostTrailNode);
        }
    }
}