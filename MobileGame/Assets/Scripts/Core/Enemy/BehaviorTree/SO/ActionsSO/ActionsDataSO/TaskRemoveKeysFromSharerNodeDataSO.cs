using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/TaskRemoveKeysFromSharerNodeDataSO",
        fileName = "new TaskRemoveKeysFromSharerNodeDataSO")]
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