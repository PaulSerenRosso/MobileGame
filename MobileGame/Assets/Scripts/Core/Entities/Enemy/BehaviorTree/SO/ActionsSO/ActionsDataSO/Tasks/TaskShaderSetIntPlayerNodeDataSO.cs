using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/ShaderSetIntPlayerNodeDataSO",
        fileName = "new Tree_T_ShaderSetIntPlayer_Spe_Data")]
    public class TaskShaderSetIntPlayerNodeDataSO : ActionNodeDataSO
    {
        public string ReferenceNameValue;
        public int IntValue;
        
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PlayerRenderer };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskShaderSetIntPlayerNode);
        }
    }
}