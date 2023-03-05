using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/CirclesAreOccupiedNodeSO",
        fileName = "new CH_CirclesAreOccupied_Spe")]
    public class CheckCirclesAreOccupiedNodeSO : CheckNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType.VECTOR3,
                    BehaviourTreeEnums.InternValuePropertyType.GET, "Destination(Vector3) of a MovePoint");
            }
        }

        public override void UpdateCommentary()
        {
            
        }
    }
}