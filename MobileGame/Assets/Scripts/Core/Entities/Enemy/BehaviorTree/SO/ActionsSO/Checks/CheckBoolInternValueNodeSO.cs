using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/Checks/BoolInternValueNodeSO",
        fileName = "new CH_BoolInternValue_Spe")]
    public class CheckBoolInternValueNodeSO : CheckNodeSO
    {
        public override void UpdateInterValues()
        {
            base.UpdateInterValues();
            _internValuesCount = 1;
            if (InternValues.Count > 0)
            {
                InternValues[0].SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType.BOOL,
                    BehaviorTreeEnums.InternValuePropertyType.GET, "Boolean(bool) to check in intern value");
            }
        }

        public override void UpdateComment()
        {
            Comment = "Nœud qui permet de vérifier la valeur boolean d'une intern value";
        }
    }
}