using System.Collections.Generic;
using UnityEngine.Serialization;

namespace BehaviorTree.SO.Composite
{
    public abstract class CompositeSO : InnerNodeSO
    {
        [FormerlySerializedAs("Childs")] public List<NodeSO> Children;
    }
}