using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/AnimatorSetFloatNodeDataSO",
        fileName = "new Tree_T_AnimatorSetFloat_Spe_Data")]
    public class TaskAnimatorSetFloatNodeDataSO : ActionNodeDataSO
    {
        public string NameParameter;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.Animator };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskAnimatorSetFloatNode);
        }
    }
}