using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/PlayerIsMovingNodeSO",
        fileName = "new  Tree_CH_PlayerIsMoving_Spe")]
    public class CheckPlayerIsMovingNodeSO : CheckNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui retourne si le joueur se déplace actuellement";
        }
    }
}