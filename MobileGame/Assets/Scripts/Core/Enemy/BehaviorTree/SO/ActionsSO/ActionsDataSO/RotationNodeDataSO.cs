using System;
using BehaviorTree.Nodes.Actions;
using BehaviorTree.SO.Actions;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorTree/Data/RotationNodeDataSO", fileName = "new RotationNodeDataSO")]
public class RotationNodeDataSO : ActionNodeDataSO
{
    public float TimeRotation;
    [Header("Rotation in degrees")]
    public float RotationAmount;
    
    protected override void SetDependencyValues()
    {
        EnemyValues = new[] { BehaviourTreeEnums.TreeEnemyValues.Transform };
    }

    public override Type GetTypeNode()
    {
        return typeof(RotationNode);
    }
}
