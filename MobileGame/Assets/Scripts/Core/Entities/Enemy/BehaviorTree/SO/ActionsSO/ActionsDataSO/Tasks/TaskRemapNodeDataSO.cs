using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/RemapNodeDataSO",
        fileName = "new T_Remap_Spe_Data")]
    public class TaskRemapNodeDataSO : ActionNodeDataSO
    {
        public float OldMinValue;
        public float OldMaxValue;
        public float NewMinValue;
        public float NewMaxValue;
        
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskRemapNode);
        }
    }
}