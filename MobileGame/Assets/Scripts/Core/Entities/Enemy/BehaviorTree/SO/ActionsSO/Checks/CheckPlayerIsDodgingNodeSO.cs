using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/PlayerIsDodgingNodeSO",
        fileName = "new  Tree_CH_PlayerIsDodging_Spe")]
    public class CheckPlayerIsDodgingNodeSO : CheckNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui retourne si le joueur est en train d'esquiver";
        }
    }
}