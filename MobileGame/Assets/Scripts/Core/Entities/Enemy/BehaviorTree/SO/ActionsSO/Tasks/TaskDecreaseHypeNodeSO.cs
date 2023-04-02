using UnityEngine;

namespace BehaviorTree.SO
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/DecreaseHypeNodeSO", fileName = "new T_DecreaseHype_Spe")]
    public class TaskDecreaseHypeNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment =
                "Fait baisser la hype en fonction de la valeur renseignée dans le DATA. Vous pouvez cocher le paramètre isUpdated en fonction de si votre node sera lancer à chaque frame ou non";
        }
    }
}