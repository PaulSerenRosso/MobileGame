using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/EvaluateCurveNodeDataSO",
        fileName = "new Tree_T_EvaluateCurve_Spe_Data")]
    public class TaskEvaluateCurveNodeDataSO : ActionNodeDataSO
    {
        public AnimationCurve Curve;
        
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskEvaluateCurveNode);
        }
    }
}