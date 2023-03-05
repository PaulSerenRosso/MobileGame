using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Tasks/GetIndexMovePointPositionNodeSO",
        fileName = "new T_GetIndexMovePointPosition_Spe")]
    public class TaskGetIndexMovePointPositionNodeSO : TaskNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.INT,
                    BehaviourTreeEnums.InternValuePropertyType.GET, "Index(int) of a MovePoint");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.VECTOR3,
                        BehaviourTreeEnums.InternValuePropertyType.SET,
                        "Destination(Vector3) of a MovePoint");
                }
            }
        }

        public override void UpdateCommentary()
        {
            
        }
    }
}