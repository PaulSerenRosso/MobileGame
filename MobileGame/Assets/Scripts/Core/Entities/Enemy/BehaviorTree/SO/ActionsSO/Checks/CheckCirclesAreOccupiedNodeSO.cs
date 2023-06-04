using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/CirclesAreOccupiedNodeSO",
        fileName = "new Tree_CH_CirclesAreOccupied_Spe")]
    public class CheckCirclesAreOccupiedNodeSO : CheckNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.VECTOR3,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Destination(Vector3) of a MovePoint");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de de vérifier si la destination est occupée";
        }
    }
}