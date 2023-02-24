using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree.Data;
using UnityEngine;

namespace BehaviorTree.Struct
{
public class ActionNodeStructSO : StructNodeSO
{
    public ActionNodeDataSO data;
    public override Type GetTypeNode()
    {
        return data.GetTypeNode();
    }
}
}
