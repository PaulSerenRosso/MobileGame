using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/MoveGridNodeSO", fileName = "new Tree_T_MoveGrid_Spe")]
    public class TaskMoveGridNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.VECTOR3,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Destination(Vector3) where the grid need to move");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                        BehaviorTreeEnums.InternValuePropertyType.GET,
                        "Index(int) the index of the movepoint where the player need to move");
                }
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui recupère les valeurs de déplacement du grid et du joueur pour effectuer les déplacements";
        }
    }
}