using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/GetTimeAnimationNodeDataSO",
        fileName = "new Tree_T_GetTimeAnimation_Spe_Data")]
    public class TaskGetTimeAnimationNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.Animator };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskGetTimeAnimationNode);
        }
    }
}