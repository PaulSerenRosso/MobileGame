using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorTree/StructSelectorSO", fileName = "new StructSelectorSO")]
public class StructSelectorSO : InnerNodeStructSO
{
    public override Type GetTypeNode()
    {
        return typeof(Selector);
    }
}
