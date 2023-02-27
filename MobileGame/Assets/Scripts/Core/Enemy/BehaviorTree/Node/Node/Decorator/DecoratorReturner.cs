using BehaviorTree.SO.Decorator;

namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorReturner : DecoratorNode
    {
        public DecoratorReturnerSO SO;

        public DecoratorReturner()
        {
            
        }

        public override NodeSO GetNodeSO()
        {
            return SO;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            SO = (DecoratorReturnerSO)nodeSO;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            Child.Evaluate();
            return SO.ReturnState;
        }
    }
}