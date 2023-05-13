using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/OpenPopupPanelNodeSO",
        fileName = "new Tree_T_OpenPopupPanel_Spe")]
    public class TaskOpenPopupPanelNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui permet d'activer une popup";
        }
    }
}