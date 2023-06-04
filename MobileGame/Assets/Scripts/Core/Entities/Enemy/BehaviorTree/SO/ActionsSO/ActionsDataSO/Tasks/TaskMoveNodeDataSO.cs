using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/MoveNodeDataSO", fileName = "new Tree_T_Move_Spe_Data")]
    public class TaskMoveNodeDataSO : ActionNodeDataSO
    {
        public AnimationCurve CurvePosition;
        public float MaxTime;

        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.Rigidbody };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskMoveNode);
        }
    }
}