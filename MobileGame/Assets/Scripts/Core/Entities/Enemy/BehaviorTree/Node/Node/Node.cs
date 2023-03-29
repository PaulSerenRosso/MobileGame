using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Nodes
{
    public abstract class Node
    {
        protected BehaviorTreeEnums.NodeState _state;

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

        public abstract IEnumerator<BehaviorTreeEnums.NodeState> Evaluate();
    }
}