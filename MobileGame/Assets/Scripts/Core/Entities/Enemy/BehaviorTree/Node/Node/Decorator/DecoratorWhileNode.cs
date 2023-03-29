using BehaviorTree.SO.Decorator;

namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorWhileNode : DecoratorNode
    {
        private DecoratorWhileSO _so;
        private BehaviorTreeEnums.NodeState _childEvaluate;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (DecoratorWhileSO)nodeSO;
        }

        public override BehaviorTreeEnums.NodeState Evaluate()
        {
            _childEvaluate = Child.Evaluate();
            return _childEvaluate == _so.WhileStateCondition ? BehaviorTreeEnums.NodeState.LOOP : _childEvaluate;
        }
    }
}