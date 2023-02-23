using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNodeStructSO : StructNodeSO
{
    public ActionNodeDataSO data;
    public override Type GetTypeNode()
    {
        return data.GetTypeNode();
    }
}
