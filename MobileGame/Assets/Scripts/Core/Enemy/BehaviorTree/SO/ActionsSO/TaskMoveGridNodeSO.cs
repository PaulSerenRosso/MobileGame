using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/TaskMoveGridNodeSO", fileName = "new TaskMoveGridNodeSO")]
    public class TaskMoveGridNodeSO : ActionNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 2;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.VECTOR3,
                    BehaviourTreeEnums.InternValuePropertyType.GET, "Destination(Vector3) where the grid need to move");
                if (InternValues.Count > 1)
                {
                    InternValues[1].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.INT,
                        BehaviourTreeEnums.InternValuePropertyType.GET,
                        "Index(int) the index of the movepoint where the player need to move");
                }
            }
        }

        public override void UpdateCommentary()
        {
            
        }
    }
}