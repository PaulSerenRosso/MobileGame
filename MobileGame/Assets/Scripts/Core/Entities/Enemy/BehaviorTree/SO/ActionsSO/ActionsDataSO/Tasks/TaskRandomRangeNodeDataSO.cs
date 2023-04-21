using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/RandomRangeNodeDataSO",
        fileName = "new T_RandomRange_Spe_Data")]
    public class TaskRandomRangeNodeDataSO : ActionNodeDataSO
    {
        public float MinValue;
        public float MaxValue;
        
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskRandomRangeNode);
        }
    }
}