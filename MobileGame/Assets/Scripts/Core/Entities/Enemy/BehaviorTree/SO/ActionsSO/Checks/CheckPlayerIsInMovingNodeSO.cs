using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/PlayerIsInMovingNodeSO",
        fileName = "new  CH_PlayerIsInMoving_Spe")]
    public class CheckPlayerIsInMovingNodeSO : CheckNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui retourne si le joueur se déplace actuellement";
        }
    }
}