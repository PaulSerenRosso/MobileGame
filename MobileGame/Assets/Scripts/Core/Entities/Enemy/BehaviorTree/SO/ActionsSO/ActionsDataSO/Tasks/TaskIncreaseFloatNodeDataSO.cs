using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/IncreaseFloatNodeDataSO",
        fileName = "new Tree_T_IncreaseFloat_Spe_Data")]
    public class TaskIncreaseFloatNodeDataSO : ActionNodeDataSO
    {
        public BehaviorTreeEnums.InternValueCalculate InternValueCalculate;
        public float StartValue;
        public float FloatValue;
        
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskIncreaseFloatNode);
        }
    }
}