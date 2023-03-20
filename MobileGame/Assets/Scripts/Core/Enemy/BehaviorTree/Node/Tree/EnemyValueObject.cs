using System;
using Object = UnityEngine.Object;

namespace BehaviorTree.Trees
{
    [Serializable]
    public class EnemyValueObject
    {
        public BehaviourTreeEnums.TreeEnemyValues Type;
        public Object Obj;
    }
}