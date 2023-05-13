using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/OpenPopupPanelNodeDataSO",
        fileName = "new Tree_T_OpenPopupPanel_Spe_Data")]
    public class TaskOpenPopupPanelNodeDataSO : ActionNodeDataSO
    {
        public BehaviorTreeEnums.PopupValue PopupValue;
        
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.UICanvasService };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskOpenPopupPanelNode);
        }
    }
}