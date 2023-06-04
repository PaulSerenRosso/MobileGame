using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/SendDebugLogNodeSO", fileName = "new Tree_T_SendDebugLogNode_Spe")]
    public class TaskSendDebugLogNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Envoyer un debug";
        }
    }
}