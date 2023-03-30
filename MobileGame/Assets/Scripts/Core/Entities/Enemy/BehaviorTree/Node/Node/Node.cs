using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Nodes
{
    public abstract class Node
    {
        public BehaviorTreeEnums.NodeState State;
        public MonoBehaviour CoroutineLauncher;
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

        public abstract IEnumerator Evaluate();
    }
}