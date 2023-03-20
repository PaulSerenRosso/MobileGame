using System.Collections.Generic;

namespace BehaviorTree.Nodes.Composite
{
    public abstract class CompositeNode : Node
    {
        public List<Node> Children = new();

        public void Attach(Node node)
        {
            Children.Add(node);
        }
    }
}