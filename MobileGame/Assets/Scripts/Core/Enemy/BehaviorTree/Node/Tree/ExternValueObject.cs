using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace BehaviorTree.Trees
{

    [Serializable]
    public class ExternValueObject
    {
        public BehaviourTreeEnums.TreeExternValues Type;
        [HideInInspector] public Object Obj;
    }
}