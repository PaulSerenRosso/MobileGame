using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/PlayerIsTauntingNodeSO",
        fileName = "new  Tree_CH_PlayerIsTaunting_Spe")]
    public class CheckPlayerIsTauntingNodeSO : CheckNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui retourne si le joueur taunt actuellement";
        }
    }
}