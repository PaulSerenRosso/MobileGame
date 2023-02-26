using System;
using BehaviorTree.Data;

namespace BehaviorTree.Struct
{
    public class ActionNodeStructSO : StructNodeSO
    {
        public ActionNodeDataSO Data;

        public override Type GetTypeNode()
        {
            return Data.GetTypeNode();
        }
    }
}