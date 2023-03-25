using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/RotationNodeDataSO", fileName = "new T_Rotation_Spe_Data")]
    public class TaskRotationNodeDataSO : ActionNodeDataSO
    {
        public float TimeRotation;
        [Header("Rotation in degrees")] public float RotationAmount;

        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.Transform };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskRotationNode);
        }
    }
}