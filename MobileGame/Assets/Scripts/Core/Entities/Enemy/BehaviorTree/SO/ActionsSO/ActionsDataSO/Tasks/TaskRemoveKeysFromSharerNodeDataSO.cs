using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/RemoveKeysFromSharerNodeDataSO",
        fileName = "new Tree_T_RemoveKeysFromSharer_Spe_Data")]
    public class TaskRemoveKeysFromSharerNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskRemoveKeysFromSharerNode);
        }
    }
}