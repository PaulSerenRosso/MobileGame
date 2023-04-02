using System.Collections.Generic;

namespace BehaviorTree.Nodes.Composite
{
    public abstract class CompositeNode : InnerNode.InnerNode
    {
        public List<Node> Children = new();
        
        public void Attach(Node node)
        {
            node.ReturnedEvent = EvaluateChild;
            Children.Add(node);
        }
    }
}