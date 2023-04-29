using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/ShaderSetFloatNodeDataSO",
        fileName = "new Tree_T_ShaderSetFloat_Spe_Data")]
    public class TaskShaderSetFloatNodeDataSO : ActionNodeDataSO
    {
        public float FloatValue;
        
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PlayerRenderer };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskShaderSetFloatNode);
        }
    }
}