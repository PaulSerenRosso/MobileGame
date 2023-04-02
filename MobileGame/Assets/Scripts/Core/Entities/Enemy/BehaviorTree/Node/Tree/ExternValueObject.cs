using System;
using UnityEngine;
using Object = System.Object;

namespace BehaviorTree.Trees
{
    [Serializable]
    public class ExternValueObject
    {
        public BehaviorTreeEnums.TreeExternValues Type;
        [HideInInspector] public Object Obj;
    }
}