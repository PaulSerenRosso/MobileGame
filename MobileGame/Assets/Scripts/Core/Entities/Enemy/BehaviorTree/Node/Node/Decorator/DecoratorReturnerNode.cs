using BehaviorTree.SO.Decorator;

namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorReturnerNode : DecoratorNode
    {
        public DecoratorReturnerSO SO;

        public override NodeSO GetNodeSO()
        {
            return SO;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            SO = (DecoratorReturnerSO)nodeSO;
        }

        public override BehaviorTreeEnums.NodeState Evaluate()
        {
            Child.Evaluate();
            return SO.ReturnState;
        }
    }
}