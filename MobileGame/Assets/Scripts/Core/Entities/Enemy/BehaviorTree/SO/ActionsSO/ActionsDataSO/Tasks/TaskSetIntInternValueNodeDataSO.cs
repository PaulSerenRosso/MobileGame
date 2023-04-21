using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SetIntInternValueNodeDataSO",
        fileName = "new T_SetIntInternValue_Spe_Data")]
    public class TaskSetIntInternValueNodeDataSO : ActionNodeDataSO
    {
        public BehaviorTreeEnums.InternValueIntCalculate InternValueIntCalculate;
        public int Value;
        public bool IsValueInternValue;

        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskSetIntInternValueNode);
        }
    }
}