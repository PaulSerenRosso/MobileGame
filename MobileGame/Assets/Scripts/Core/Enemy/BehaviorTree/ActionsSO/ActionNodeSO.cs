using System;
using BehaviorTree.ActionsSO;

namespace BehaviorTree
{
    public abstract class ActionNodeSO : NodeSO
    {
        public ActionNodeDataSO Data;

        public override Type GetTypeNode()
        {
            return Data.GetTypeNode();
        }

        public virtual int[] GetHashCodeKeyOfInternValue()
        {
            return null;
        }

        public virtual void ConvertKeyOfInternValueToHashCode()
        {
            
        }

        private void OnValidate()
        {
            ConvertKeyOfInternValueToHashCode();
        }
    }
}