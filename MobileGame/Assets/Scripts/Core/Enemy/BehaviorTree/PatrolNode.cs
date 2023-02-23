using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class PatrolNode : ActionNode
{
    public PatrolNodeDataSO data;
    public override void SetDataSO(ActionNodeDataSO so)
    {
        data = (PatrolNodeDataSO)so;
    }

    public override (Enums.TreeEnemyValues[] enemyValues, Enums.TreeExternValues[] externValues) GetDependencyValues()
    {
        throw new System.NotImplementedException();
    }

    public override void SetDependencyValues(params object[] dependencyValues)
    {
        throw new System.NotImplementedException();
    }

    public override NodeState Evaluate()
    {
        return NodeState.FAILURE;
    }
}
