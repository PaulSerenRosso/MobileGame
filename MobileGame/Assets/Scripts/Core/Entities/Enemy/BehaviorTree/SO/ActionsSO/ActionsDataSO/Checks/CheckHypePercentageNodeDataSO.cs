using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/HypePercentageNodeDataSO",
        fileName = "new Tree_CH_HypePercentage_Spe_Data")]
    public class CheckHypePercentageNodeDataSO : ActionNodeDataSO
    {
        public float PercentageCompare;
        
        protected override void SetDependencyValues()
        {
            ExternValues = new[]
            {
                BehaviorTreeEnums.TreeExternValues.HypeService
            };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckHypePercentageNode);
        }
    }
}