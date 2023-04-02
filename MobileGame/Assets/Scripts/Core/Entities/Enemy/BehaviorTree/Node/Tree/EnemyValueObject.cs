using System;
using Object = UnityEngine.Object;

namespace BehaviorTree.Trees
{
    [Serializable]
    public class EnemyValueObject
    {
        public BehaviorTreeEnums.TreeEnemyValues Type;
        public Object Obj;
    }
}