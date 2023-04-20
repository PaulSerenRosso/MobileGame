using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/UltimateHypeNodeDataSO",
        fileName = "new CH_UltimateHype_Spe_Data")]
    public class CheckUltimateHypeNodeDataSO: ActionNodeDataSO
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
            return typeof(CheckUltimateHypeNode);
        }
    }
}