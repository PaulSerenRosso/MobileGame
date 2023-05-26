using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/ActivationFXNodeDataSO",
        fileName = "new Tree_T_ActivationFX_Spe_Data")]
    public class TaskActivationFXNodeDataSO : ActionNodeDataSO
    {
        public bool IsColorChanged;
        public Color ParticleColor;
        public BehaviorTreeEnums.TreeEnemyValues[] FXEnumValues;
        public bool IsDirectionChanged;
        public Vector2 ParticleDirection;
        protected override void SetDependencyValues()
        {
            EnemyValues = new BehaviorTreeEnums.TreeEnemyValues[FXEnumValues.Length+1];
            for (int i = 0; i < EnemyValues.Length-1; i++)
            {
                EnemyValues[i] = FXEnumValues[i];
            }
            EnemyValues[^1] = BehaviorTreeEnums.TreeEnemyValues.Transform;
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskActivationFXNode);
        }
    }
}