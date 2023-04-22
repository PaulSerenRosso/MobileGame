using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/AnimatorSetBoolNodeDataSO",
        fileName = "new Tree_T_AnimatorSetBool_Spe_Data")]
    public class TaskAnimatorSetBoolNodeDataSO : ActionNodeDataSO
    {
        public bool ValueToPass;
        public string NameParameter; 
            
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.Animator };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskAnimatorSetBoolNode);
        }
    }
}