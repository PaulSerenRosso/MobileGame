using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/TaskMoveNodeDataSO", fileName = "new TaskMoveNodeDataSO")]
    public class TaskMoveNodeDataSO : ActionNodeDataSO
    {
        public AnimationCurve CurvePosition;
        public float MaxTime;

        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviourTreeEnums.TreeEnemyValues.Rigidbody };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskMoveNode);
        }
    }
}