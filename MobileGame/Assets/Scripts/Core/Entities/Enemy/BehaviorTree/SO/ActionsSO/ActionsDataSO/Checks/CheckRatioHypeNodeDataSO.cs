using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/RatioHypeDataSO",
        fileName = "new CH_RatioHype_Spe_Data")]
    public class CheckRatioHypeNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[]
            {
                BehaviorTreeEnums.TreeExternValues.HypeService
            };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckRatioHypeNode);
        }
    }
}