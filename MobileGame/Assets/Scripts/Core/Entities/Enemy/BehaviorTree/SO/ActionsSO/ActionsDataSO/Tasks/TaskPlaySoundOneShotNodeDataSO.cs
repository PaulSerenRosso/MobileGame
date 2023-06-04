using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/PlaySoundOneShotNodeDataSO",
        fileName = "new Tree_T_PlaySoundOneShot_Spe_Data")]
    public class TaskPlaySoundOneShotNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskPlaySoundOneShotNode);
        }
    }
}