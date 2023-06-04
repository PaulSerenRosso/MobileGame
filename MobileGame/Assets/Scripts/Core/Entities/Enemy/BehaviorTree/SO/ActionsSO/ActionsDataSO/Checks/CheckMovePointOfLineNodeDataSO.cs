using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/MovePointOfLineDataSO", fileName = "new Tree_CH_MovePointOfLine_Spe_Data")]
    public class CheckMovePointOfLineNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[]
            {
                BehaviorTreeEnums.TreeExternValues.GridManager
            };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckMovePointOfLineNode);
        }
    }
}