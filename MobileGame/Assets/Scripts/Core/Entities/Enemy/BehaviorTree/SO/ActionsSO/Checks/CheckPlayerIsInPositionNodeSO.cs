using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/PlayerIsInPositionNodeSO",
        fileName = "new  Tree_CH_PlayerIsInPosition_Spe")]
    public class CheckPlayerIsInPositionNodeSO : CheckNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.VECTOR3LIST,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Vector3(List<Vector3>) vector3 positions");
            }
        }
        
        public override void UpdateComment()
        {
            Comment = "Nœud qui vérifie si le joueur est proche de la position";
        }
    }
}