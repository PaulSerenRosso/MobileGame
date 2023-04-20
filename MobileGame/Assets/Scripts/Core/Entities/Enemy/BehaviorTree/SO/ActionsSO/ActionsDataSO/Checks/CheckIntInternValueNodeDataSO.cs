using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/IntInternValueDataSO",
        fileName = "new CH_IntInternValue_Spe_Data")]
    public class CheckIntInternValueNodeDataSO : ActionNodeDataSO
    {
        public int ValueToCheck;
        
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckIntInternValueNode);
        }
    }
}