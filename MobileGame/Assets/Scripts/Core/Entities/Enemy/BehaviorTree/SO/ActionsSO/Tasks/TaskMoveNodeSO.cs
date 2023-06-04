using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/MoveNodeSO", fileName = "new Tree_T_Move_Spe")]
    public class TaskMoveNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.VECTOR3,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Destination(int) of a MovePoint");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de déplacer un ennemi à la position qu'il reçoit";
        }
    }
}