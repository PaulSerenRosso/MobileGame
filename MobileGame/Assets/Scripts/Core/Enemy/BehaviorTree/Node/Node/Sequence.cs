using System.Collections.Generic;

namespace BehaviorTree.Nodes
{
    public class Sequence : Node
    {
        public Sequence() { }
        
        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (Node node in _children)
            {
                switch (node.Evaluate())
                {
                    case BehaviourTreeEnums.NodeState.FAILURE:
                        _state = BehaviourTreeEnums.NodeState.FAILURE;
                        return _state;
                    case BehaviourTreeEnums.NodeState.SUCCESS:
                        continue;
                    case BehaviourTreeEnums.NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        _state = BehaviourTreeEnums.NodeState.SUCCESS;
                        return _state;
                }
            }

            _state = anyChildIsRunning ? BehaviourTreeEnums.NodeState.RUNNING : BehaviourTreeEnums.NodeState.SUCCESS;
            return _state;
        }
    }
}