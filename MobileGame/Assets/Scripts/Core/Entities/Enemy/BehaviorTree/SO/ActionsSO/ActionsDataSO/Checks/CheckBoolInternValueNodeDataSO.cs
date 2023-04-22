using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/BoolInternValueNodeDataSO",
        fileName = "new Tree_CH_BoolInternValue_Spe_Data")]
    public class CheckBoolInternValueNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckBoolInternValueNode);
        }
    }
}