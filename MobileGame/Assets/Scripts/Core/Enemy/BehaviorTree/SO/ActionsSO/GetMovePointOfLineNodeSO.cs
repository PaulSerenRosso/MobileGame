using BehaviorTree;
using UnityEngine;

namespace Core.Enemy.BehaviorTree.SO.ActionsSO
{
    [CreateAssetMenu(menuName = "BehaviorTree/Actions/GetMovePointOfLineNodeSO",
        fileName = "new  GetMovePointOfLineNodeSO")]
    public class GetMovePointOfLineNodeSO : ActionNodeSO
    {
        public StringWithHashCode StartIndexKey;
        public StringWithHashCode ResultIndexKey;

        public override void ConvertKeyOfInternValueToHashCode()
        {
            StartIndexKey.UpdateKeyHashCode();
            ResultIndexKey.UpdateKeyHashCode();
        }
    }
}