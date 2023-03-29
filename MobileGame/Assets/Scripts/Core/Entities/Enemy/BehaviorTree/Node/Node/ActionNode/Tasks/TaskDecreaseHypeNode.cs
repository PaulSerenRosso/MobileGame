
using System;
using System.Collections.Generic;
using BehaviorTree.SO;
using BehaviorTree.SO.Actions;
using Service.Hype;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
public class TaskDecreaseHypeNode : ActionNode
{

    private TaskDecreaseHypeNodeSO _so;
    private TaskDecreaseHypeNodeDataSO _data;
    private event Func<float> _calculateDecreaseAmountEvent;
    private IHypeService _hypeService; 
    public override BehaviorTreeEnums.NodeState Evaluate()
    {
        _hypeService.DecreaseHype(_calculateDecreaseAmountEvent.Invoke());
        return BehaviorTreeEnums.NodeState.RUNNING;
    }

    public override void SetDependencyValues(Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues, Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
    {
        _hypeService = (IHypeService)externDependencyValues[BehaviorTreeEnums.TreeExternValues.HypeService];
    }

    public override ActionNodeDataSO GetDataSO()
    {
        return _data;
    }

    public override NodeSO GetNodeSO()
    {
        return _so;
    }

    public override void SetNodeSO(NodeSO nodeSO)
    {
        _so =(TaskDecreaseHypeNodeSO) nodeSO;
        _data =(TaskDecreaseHypeNodeDataSO) _so.Data;
        if (_data.IsUpdated)
        {
            _calculateDecreaseAmountEvent += CalculateDecreaseAmountWithDeltatime;
        }
        else
        {
            _calculateDecreaseAmountEvent += CalculateDecreaseAmount;
        }
            
    }

    private float CalculateDecreaseAmountWithDeltatime() => _data.HypeAmount * Time.deltaTime;
    

    private float CalculateDecreaseAmount() => _data.HypeAmount;
}
    
}
