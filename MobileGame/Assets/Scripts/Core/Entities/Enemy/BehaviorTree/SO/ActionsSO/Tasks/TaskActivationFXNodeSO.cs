using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/ActivationFXNodeSO", fileName = "new T_ActivationFX_Spe")]
    public class TaskActivationFXNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui s'occupe d'activer des FX sur le boss";
        }
    }
}