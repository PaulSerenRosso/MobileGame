using System.Collections.Generic;

namespace BehaviorTree.SO.Composite
{
    public abstract class CompositeSO : InnerNodeSO
    {
        public List<NodeSO> Childs;
    }
}