using BehaviorTree.SO.Decorator;

namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorUntilNode : DecoratorNode
    {
        private DecoratorUntilSO _so;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (DecoratorUntilSO)nodeSO;
        }

        public override BehaviorTreeEnums.NodeState Evaluate()
        {
            return Child.Evaluate() == _so.BreakStateCondition ? _so.BreakStateCondition : BehaviorTreeEnums.NodeState.LOOP;
        }
    }
}