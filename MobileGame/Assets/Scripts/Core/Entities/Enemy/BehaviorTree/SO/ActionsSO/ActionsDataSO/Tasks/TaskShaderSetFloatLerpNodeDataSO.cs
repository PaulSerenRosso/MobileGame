using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/ShaderSetFloatLerpNodeDataSO",
        fileName = "new Tree_T_ShaderSetFloatLerp_Spe_Data")]
    public class TaskShaderSetFloatLerpNodeDataSO : ActionNodeDataSO
    {
        public float MinFloatValue;
        public float MaxFloatValue;
        
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskShaderSetFloatLerpNode);
        }
    }
}