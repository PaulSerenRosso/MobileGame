using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/MoveHypeNodeSO", fileName = "new Tree_T_MoveHype_Spe")]
    public class TaskMoveHypeNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment =
                "Fait bouger la hype en fonction de la fonction choisie ainsi que de la valeur renseignée dans le DATA. Vous pouvez cocher le paramètre isUpdated en fonction de si votre node sera lancer à chaque frame ou non";
        }
    }
}