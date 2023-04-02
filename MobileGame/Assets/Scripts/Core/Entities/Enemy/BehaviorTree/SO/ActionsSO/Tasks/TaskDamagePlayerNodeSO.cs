using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/DamagePlayerNodeSO", fileName = "new T_DamagePlayer_Spe")]
    public class TaskDamagePlayerNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui s'occupe d'effectuer les dégats au joueur";
        }

        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
        }
    }
}