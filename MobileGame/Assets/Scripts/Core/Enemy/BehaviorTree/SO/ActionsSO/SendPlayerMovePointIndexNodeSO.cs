using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    public class SendPlayerMovePointIndexNodeSO : ActionNodeSO
    {
        public StringWithHashCode _playerMovePointIndexKey = new();

        public override void ConvertKeyOfInternValueToHashCode()
        {
            _playerMovePointIndexKey.UpdateKeyHashCode();
        }
    }
}