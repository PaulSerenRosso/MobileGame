using System;
using System.Collections.Generic;
using BehaviorTree.Struct;

namespace BehaviorTree.Nodes
{
   

    public abstract class Node
    {
        public Node Parent;

        protected BehaviourTreeEnums.NodeState _state;
        public List<Node> _children = new();

        private Dictionary<string, object> _dataContext = new();


        public static Node CreateNodeSO(StructNodeSO so)
        {
          return (Node) Activator.CreateInstance(so.GetTypeNode());
        }
        public Node()
        {
            Parent = null;
        }
        
        public void Attach(Node node)
        {
            node.Parent = this;
            _children.Add(node);
        }

        public abstract BehaviourTreeEnums.NodeState Evaluate();

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            if (_dataContext.TryGetValue(key, out var value))
                return value;

            Node node = Parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.Parent;
            }

            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = Parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.Parent;
            }

            return false;
        }
    }
}