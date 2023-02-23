using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BehaviorTree/PatrolNodeDataSO", fileName = "new PatrolNodeDataSO")]
public class PatrolNodeDataSO : TaskDataSO
{
    public override Type GetTypeNode()
    {
        return typeof(PatrolNode);
    }
}
