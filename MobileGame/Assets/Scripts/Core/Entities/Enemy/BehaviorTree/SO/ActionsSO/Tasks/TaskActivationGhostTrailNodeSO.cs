using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/ActivationGhostTrailNodeSO",
        fileName = "new Tree_T_ActivationGhostTrail_Spe")]
    public class TaskActivationGhostTrailNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui s'occupe d'activer le ghost trail sur le boss";
        }
    }
}