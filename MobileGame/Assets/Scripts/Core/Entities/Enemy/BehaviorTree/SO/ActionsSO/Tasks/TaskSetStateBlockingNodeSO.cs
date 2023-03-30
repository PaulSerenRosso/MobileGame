using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/SetStateBlockingNodeSO",
        fileName = "new  T_SetStateBlocking_Spe")]
    public class TaskSetStateBlockingNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui modifie le statut de \"Blocking\"";
        }
    }
}