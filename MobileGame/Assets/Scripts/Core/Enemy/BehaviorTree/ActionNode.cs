using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using Object = System.Object;

public abstract class ActionNode : Node
{
    public abstract void SetDataSO(ActionNodeDataSO so);

    public abstract (Enums.TreeEnemyValues[] enemyValues, Enums.TreeExternValues[] externValues) GetDependencyValues();

    public abstract void SetDependencyValues(params Object[] dependencyValues);

}
