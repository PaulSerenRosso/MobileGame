using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;
using UnityEngine.Serialization;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SetIntInternValueNodeDataSO",
        fileName = "new Tree_T_SetIntInternValue_Spe_Data")]
    public class TaskSetIntInternValueNodeDataSO : ActionNodeDataSO
    {
        [FormerlySerializedAs("InternValueIntCalculate")] public BehaviorTreeEnums.InternValueCalculate InternValueCalculate;
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