using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/ShaderSetIntNodeDataSO",
        fileName = "new Tree_T_ShaderSetInt_Spe_Data")]
    public class TaskShaderSetIntNodeDataSO : ActionNodeDataSO
    {
        public string ReferenceNameValue;
        public int IntValue;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.EnemyManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskShaderSetIntNode);
        }
    }
}