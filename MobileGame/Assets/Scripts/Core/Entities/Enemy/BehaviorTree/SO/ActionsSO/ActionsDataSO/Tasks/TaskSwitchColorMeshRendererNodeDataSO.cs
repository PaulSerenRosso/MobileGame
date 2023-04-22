using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SwitchColorMeshRendererNodeDataSO",
        fileName = "new Tree_T_SwitchColorMeshRenderer_Spe_Data")]
    public class TaskSwitchColorMeshRendererNodeDataSO : ActionNodeDataSO
    {
        public Color SwitchableColor;

        public override Type GetTypeNode()
        {
            return typeof(TaskSwitchColorMeshRendererNode);
        }

        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.MeshRenderer };
        }
    }
}