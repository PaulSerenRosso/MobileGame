namespace BehaviorTree.Nodes.Decorator
{
    public abstract class DecoratorNode : InnerNode.InnerNode
    {
        public Node Child;

        public void Attach(Node node)
        {
            Child = node;
            Child.ReturnedEvent = EvaluateChild;
        }
    }
}