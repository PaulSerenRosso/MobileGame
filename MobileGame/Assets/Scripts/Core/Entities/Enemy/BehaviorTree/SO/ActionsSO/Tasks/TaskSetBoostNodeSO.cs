using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/SetBoostNodeSO",
        fileName = "new Tree_T_SetBoost_Spe")]
    public class TaskSetBoostNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de SET le boost de notre boss pour activer les reductions de damage";
        }
    }
}