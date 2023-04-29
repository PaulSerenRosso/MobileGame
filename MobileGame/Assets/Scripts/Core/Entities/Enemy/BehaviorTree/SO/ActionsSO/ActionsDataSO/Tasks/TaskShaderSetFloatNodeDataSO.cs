using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/ShaderSetFloatNodeDataSO",
        fileName = "new Tree_T_ShaderSetFloat_Spe_Data")]
    public class TaskShaderSetFloatNodeDataSO : ActionNodeDataSO
    {
        public string ReferenceNameValue;
        public float FloatValue;
        public bool IsInternReferenceNameValue;
        public bool IsInternFloatValue;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.EnemyManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskShaderSetFloatNode);
        }
    }
}