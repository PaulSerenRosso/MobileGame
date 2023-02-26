using System;

namespace BehaviorTree.Nodes
{
    public abstract class Node
    {
        protected BehaviourTreeEnums.NodeState _state;

        public static Node CreateNodeSO(NodeSO so)
        {
            return (Node)Activator.CreateInstance(so.GetTypeNode());
        }

        public Node()
        {
            
        }

        public abstract BehaviourTreeEnums.NodeState Evaluate();
    }
}