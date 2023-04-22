using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/PlayerIsInMovingNodeSO",
        fileName = "new  Tree_CH_PlayerIsInMoving_Spe")]
    public class CheckPlayerIsInMovingNodeSO : CheckNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui retourne si le joueur se déplace actuellement";
        }
    }
}