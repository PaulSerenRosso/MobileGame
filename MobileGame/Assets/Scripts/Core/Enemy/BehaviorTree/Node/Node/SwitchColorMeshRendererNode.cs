using System.Collections.Generic;
using BehaviorTree.ActionsSO;
using UnityEngine;

namespace BehaviorTree.Nodes
{
    public class SwitchColorMeshRendererNode : ActionNode
    {
        private SwitchColorMeshRendererNodeDataSO _dataSo;
        private MeshRenderer _meshRenderer;

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            _meshRenderer.material.color = _dataSo.SwitchableColor;
            return BehaviourTreeEnums.NodeState.RUNNING;
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _dataSo;
        }

        public override void SetDataSO(ActionNodeDataSO dataSO)
        {
            _dataSo = (SwitchColorMeshRendererNodeDataSO)dataSO;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _meshRenderer = (MeshRenderer)enemyDependencyValues[BehaviourTreeEnums.TreeEnemyValues.MeshRenderer];
        }

        public override void SetHashCodeKeyOfInternValues(int[] hashCodeKey)
        {
            throw new System.NotImplementedException();
        }
    }
}