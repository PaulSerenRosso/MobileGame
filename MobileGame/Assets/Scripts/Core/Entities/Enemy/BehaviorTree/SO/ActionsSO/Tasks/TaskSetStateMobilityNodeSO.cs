using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/SetStateMobilityNodeSO",
        fileName = "new  Tree_T_SetStateMobility_Spe")]
    public class TaskSetStateMobilityNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui modifie le statut de \"Mobility\"";
        }
    }
}