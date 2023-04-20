using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/CallUltimateHypeNodeSO",
        fileName = "new T_CallUltimateHype_Spe")]
    public class TaskCallUltimateHypeNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de lancer l'ultime de l'enemy";
        }
    }
}