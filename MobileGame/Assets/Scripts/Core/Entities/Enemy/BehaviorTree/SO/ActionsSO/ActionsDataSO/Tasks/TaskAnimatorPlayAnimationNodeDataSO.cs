using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/AnimatorPlayAnimationNodeDataSO",
        fileName = "new T_AnimatorPlayAnimation_Spe_Data")]
    public class TaskAnimatorPlayAnimationNodeDataSO : ActionNodeDataSO
    {
        public string NameParameter;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.Animator };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskAnimatorPlayAnimationNode);
        }
    }
}