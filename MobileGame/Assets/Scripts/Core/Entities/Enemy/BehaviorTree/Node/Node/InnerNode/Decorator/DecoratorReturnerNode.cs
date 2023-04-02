using BehaviorTree.SO.Decorator;

namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorReturnerNode : DecoratorNode
    {
        private DecoratorReturnerSO _so;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (DecoratorReturnerSO)nodeSO;
        }

        public override void Evaluate()
        {
            Child.Evaluate();
        }

        public override void EvaluateChild()
        {
            State = _so.ReturnState;
            ReturnedEvent?.Invoke();
        }
    }
}