using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/ActivateVibrationNodeSO",
        fileName = "new Tree_T_ActivateVibration_Spe")]
    public class TaskActivateVibrationNodeSO: TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui s'occupe d'activer les vibrations sur téléphone";
        }
    }
}