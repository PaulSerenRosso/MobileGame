using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/GetMovePointOfLineNodeSO", fileName = "new  GetMovePointOfLineNodeSO")]
    public class GetMovePointOfLineNodeSO : ActionNodeSO
    {
        public StringWithHashCode StartIndexKey = new();
        public StringWithHashCode ResultIndexKey = new();

        public override void ConvertKeyOfInternValueToHashCode()
        {
            StartIndexKey.UpdateKeyHashCode();
            ResultIndexKey.UpdateKeyHashCode();
        }
    }
}