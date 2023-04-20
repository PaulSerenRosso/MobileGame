using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/DecreaseHypeNodeSO", fileName = "new T_DecreaseHype_Spe")]
    public class TaskMoveHypeNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment =
                "Fait bouger la hype en fonction de la fonction choisie ainsi que de la valeur renseignée dans le DATA. Vous pouvez cocher le paramètre isUpdated en fonction de si votre node sera lancer à chaque frame ou non";
        }
    }
}