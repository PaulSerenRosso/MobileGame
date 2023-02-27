namespace BehaviorTree.Nodes.Decorator
{
    public abstract class DecoratorNode : Node
    {
        public Node Child;
        
        public void Attach(Node node)
        {
            Child = node;
        }
    }
}