using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/RotationNodeDataSO", fileName = "new RotationNodeDataSO")]
    public class RotationNodeDataSO : ActionNodeDataSO
    {
        public float TimeRotation;
        [Header("Rotation in degrees")] public float RotationAmount;

        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviourTreeEnums.TreeEnemyValues.Transform };
        }

        public override Type GetTypeNode()
        {
            return typeof(RotationNode);
        }
    }
}