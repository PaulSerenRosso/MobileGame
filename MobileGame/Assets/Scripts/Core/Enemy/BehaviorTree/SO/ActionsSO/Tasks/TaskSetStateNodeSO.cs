using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/SetStateNodeSO",
        fileName = "new  T_SetState_Spe")]
    public class TaskSetStateNodeSO : ActionNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Modifier la state actuelle par la state renseignée";
        }
    }
}