using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.SO
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/DecreaseNodeSO", fileName = "new T_DecreaseNode_Spe")]
    public class TaskDecreaseHypeNodeSO : TaskNodeSO
    {
        public override void UpdateComment()
        {
            Comment =
                "Fait baisser la hype en fonction de la valeur renseignée dans le DATA. Vous pouvez cocher le paramètre isUpdated en fonction de si votre node sera lancer à chaque frame ou non";
        }
    }
}