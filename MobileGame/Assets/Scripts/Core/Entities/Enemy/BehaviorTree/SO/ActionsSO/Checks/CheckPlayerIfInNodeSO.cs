using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/PlayerIfInNodeSO",
        fileName = "new  CH_PlayerIfIn_Spe")]
    public class CheckPlayerIfInNodeSO : CheckNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.INT,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Index(Int) of a MovePoint");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui retourne si le joueur est dans les points reçus";
        }
    }
}