using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/StunPlayerNodeSO",
        fileName = "new Tree_T_StunPlayer_Spe")]
    public class TaskStunPlayerNodeSO: TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de stun le joueur";
        }
    }
}