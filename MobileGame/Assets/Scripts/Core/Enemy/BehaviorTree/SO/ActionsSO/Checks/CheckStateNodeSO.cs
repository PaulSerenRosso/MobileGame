using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/StateNodeSO",
        fileName = "new  CH_State_Spe")]
    public class CheckStateNodeSO : CheckNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Vérifier que la state actuelle correspond à la state renseignée";
        }
    }
}