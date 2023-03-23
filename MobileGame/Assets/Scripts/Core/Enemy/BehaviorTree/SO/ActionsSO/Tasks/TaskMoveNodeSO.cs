using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/MoveNodeSO", fileName = "new T_Move_Spe")]
    public class TaskMoveNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.VECTOR3,
                    BehaviourTreeEnums.InternValuePropertyType.GET, "Destination(int) of a MovePoint");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de déplacer un ennemi à la position qu'il reçoit";
        }
    }
}