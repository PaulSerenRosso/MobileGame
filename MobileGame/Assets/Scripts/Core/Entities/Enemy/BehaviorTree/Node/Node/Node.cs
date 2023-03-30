using System;

namespace BehaviorTree.Nodes
{
    public abstract class Node
    {
        public BehaviorTreeEnums.NodeState State;
        public Action ReturnedEvent;

        public static Node CreateNodeSO(NodeSO so)
        {
            return (Node)Activator.CreateInstance(so.GetTypeNode());
        }

        public Node()
        {
            
        }

        public virtual NodeSO GetNodeSO()
        {
            return null;
        }

        public virtual void SetNodeSO(NodeSO nodeSO)
        {
        }

        public abstract void Evaluate();
    }
}