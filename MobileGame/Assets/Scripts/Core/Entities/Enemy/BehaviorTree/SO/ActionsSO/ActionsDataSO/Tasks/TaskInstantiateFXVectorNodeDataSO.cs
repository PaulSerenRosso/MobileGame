﻿using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/InstantiateFXVectorNodeDataSO",
        fileName = "new Tree_T_InstantiateFXVector_Spe_Data")]
    public class TaskInstantiateFXVectorNodeDataSO : ActionNodeDataSO
    {
        public GameObject ParticleGO;
        public int Count;
        
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskInstantiateFXVectorNode);
        }
    }
}