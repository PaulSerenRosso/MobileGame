using System;
using BehaviorTree.Nodes;
using UnityEngine;

namespace BehaviorTree.ActionsSO
{
    [CreateAssetMenu(menuName = "BehaviorTree/SwitchColorMeshRendererNodeDataSO",
        fileName = "new SwitchColorMeshRendererNodeData")]
    public class SwitchColorMeshRendererNodeDataSO : ActionNodeDataSO
    {
        public Color SwitchableColor;

        public override Type GetTypeNode()
        {
            return typeof(SwitchColorMeshRendererNode);
        }

        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviourTreeEnums.TreeEnemyValues.MeshRenderer };
        }
    }
}