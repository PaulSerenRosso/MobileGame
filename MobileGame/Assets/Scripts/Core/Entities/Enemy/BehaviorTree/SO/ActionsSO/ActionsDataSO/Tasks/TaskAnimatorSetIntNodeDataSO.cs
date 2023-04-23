using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/AnimatorSetIntNodeDataSO",
        fileName = "new Tree_T_AnimatorSetInt_Spe_Data")]
    public class TaskAnimatorSetIntNodeDataSO : ActionNodeDataSO
    {
        public string NameParameter;
        public int ValueToPass;
        public bool IsValueIntern;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.Animator };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskAnimatorSetIntNode);
        }
    }
}